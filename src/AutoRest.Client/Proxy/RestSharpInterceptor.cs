#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoRest.Client.Client;
using AutoRest.Client.Processing;
using AutoRest.Client.Processing.Requests;
using AutoRest.Client.Processing.Response;
using Castle.DynamicProxy;
using RestSharp;

namespace AutoRest.Client.Proxy
{
    internal sealed class RestSharpInterceptor<TClient> : IInterceptor
    {
        private readonly RestClientConfigurationProvider<TClient> _provider;

        private readonly IRestClient _client;

        private readonly IEnumerable<object> _middlewares;

        private Queue<Func<ExecutionContext, Task>> _pipeline;

        public RestSharpInterceptor(RestClientConfigurationProvider<TClient> provider)
        {
            _provider = provider;
            _client = new RestClient();
            provider.Configuration.ClientConfiguration?.Invoke(_client);

            _middlewares = provider.Middlewares;
            _pipeline = new Queue<Func<ExecutionContext, Task>>();
        }

        public void Intercept(IInvocation invocation)
        {
            var prop = GetPropertyOfGetter(invocation);

            if (prop != null)
            {
                invocation.ReturnValue = GetEndpoint(prop, invocation);
                return;
            }

            var (isAsync, returnValue) = GetExecutionInfo(invocation.Method);

            var methodName = isAsync
                ? nameof(ExecuteAsync)
                : nameof(Execute);

            var method = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance)?.MakeGenericMethod(returnValue)
                         ?? throw new InvalidOperationException();

            invocation.ReturnValue = method.Invoke(this, new object[]
            {
                invocation
            });
        }

        private object GetEndpoint(PropertyInfo propertyInfo, IInvocation invocation)
        {
            var returnType = invocation.Method.ReturnType;
            var builderType = typeof(AutoRestClientBuilder<>).MakeGenericType(returnType);
            var builder = Activator.CreateInstance(builderType);

            var providerType = typeof(RestClientConfigurationProvider<>).MakeGenericType(returnType);
            var provider = Activator.CreateInstance(providerType, _provider.Configuration, _provider.Middlewares);

            ReflectionUtils.InvokeMethod(builder, nameof(AutoRestClientBuilder<object>.WithConfiguration), provider);

            return ReflectionUtils.InvokeMethod(builder, nameof(AutoRestClientBuilder<object>.Build));
        }

        private static PropertyInfo? GetPropertyOfGetter(IInvocation invocation)
        {
            return invocation.Method.DeclaringType?
                .GetProperties()
                .FirstOrDefault(x => x.GetGetMethod() == invocation.Method);
        }

        private static Tuple<bool, Type> GetExecutionInfo(MethodInfo invocationMethod)
        {
            var returnType = invocationMethod.ReturnType;
            var isAsync = typeof(Task).IsAssignableFrom(invocationMethod.ReturnType);

            if (isAsync)
                returnType = returnType.IsGenericType
                    ? returnType.GetGenericArguments().First()
                    : typeof(void);

            return new Tuple<bool, Type>(isAsync, returnType);
        }

        private TResponse Execute<TResponse>(IInvocation context)
        {
            return ExecuteAsync<TResponse>(context).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private async Task<TResponse> ExecuteAsync<TResponse>(IInvocation invocation)
        {
            _pipeline = new Queue<Func<ExecutionContext, Task>>();

            _pipeline.Enqueue(async context =>
            {
                context.Request = await GetRestRequest(invocation);
                await NextAsync(context);
            });

            foreach (var middleware in _middlewares)
            {
                switch (middleware)
                {
                    case AsyncRestCallMiddleware asyncMiddleware:
                        _pipeline.Enqueue(async context => await asyncMiddleware.InvokeAsync(context, NextAsync));
                        break;
                    case RestCallMiddleware syncMiddleware:
                        _pipeline.Enqueue(context =>
                        {
                            syncMiddleware.Invoke(context, NextSync);
                            return Task.CompletedTask;
                        });
                        break;
                }
            }

            _pipeline.Enqueue(async context =>
            {
                context.Response = await _client.ExecuteAsync(context.Request);
            });

            var executionContext = new ExecutionContext
            {
                ClientType = invocation.Method.DeclaringType,
                Deserializer = new ResponseDeserializer(_client)
            };

            await NextAsync(executionContext);

            return await GetReturnValue<TResponse>(_client, executionContext.Response, invocation);
        }

        private void NextSync(ExecutionContext obj)
        {
            NextAsync(obj).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private async Task NextAsync(ExecutionContext context)
        {
            if (_pipeline.TryDequeue(out var next))
                await next(context);
        }

        private static async Task<TResponse> GetReturnValue<TResponse>(IRestClient client, IRestResponse response, IInvocation invocation)
        {
            var context = new ResponseParameterBindingContext(response, new ResponseDeserializer(client),
                typeof(TResponse), default(TResponse));

            await ProcessingUtils.ApplyResponseParameterBindingAttributes(invocation.Method, context);

#pragma warning disable 8603
#pragma warning disable 8605
            return (TResponse)context.ReturnValue;
#pragma warning restore 8605
#pragma warning restore 8603
        }

        private static async Task<IRestRequest> GetRestRequest(IInvocation invocation)
        {
            var context = new RequestExecutionContext
            {
                Method = invocation.Method,
                RestRequest = new RestRequest()
            };

            await ProcessingUtils.ApplyRequestModifiersAttributes(context.Method.DeclaringType, context);
            await ProcessingUtils.ApplyRequestModifiersAttributes(context.Method, context);

            var parameters = GetParameters(invocation);

            foreach (var bindingContext in parameters)
            {
                bindingContext.ExecutionContext = context;
                await ProcessingUtils.ApplyRequestParameterBindingAttributes(bindingContext.MemberAttributes, bindingContext);
            }

            return context.RestRequest;
        }

        private static IEnumerable<RequestParameterBindingContext> GetParameters(IInvocation invocation)
        {
            var parameters = invocation.Method.GetParameters();

            var parametersContext = new List<RequestParameterBindingContext>();

            for (var i = 0; i < parameters.Length; i++)
            {
                var parameterInfo = parameters[i];
                var parameterValue = invocation.Arguments[i];

                parametersContext.Add(new RequestParameterBindingContext
                {
                    MemberType = parameterInfo.ParameterType,
                    MemberName = parameterInfo.Name,
                    MemberValue = parameterValue,
                    MemberAttributes = parameterInfo
                });
            }

            return parametersContext;
        }
    }
}