using System;
using System.Collections.Generic;
using System.Linq;
using AutoRest.Client.Processing;

namespace AutoRest.Client.Client
{
    public class RestClientConfigurationProvider<TClient>
    {
        public RestClientConfiguration Configuration { get; }

        public IEnumerable<object> Middlewares { get; }

        public RestClientConfigurationProvider(RestClientConfiguration configuration)
        {
            Configuration = configuration;
            Middlewares = ScanForMiddlewares(Configuration, Configuration.ValueResolver ?? new DefaultValueResolver());
        }

        public RestClientConfigurationProvider(RestClientConfiguration configuration, IEnumerable<object> middlewares)
        {
            Configuration = configuration;
            Middlewares = middlewares;
        }

        private static IEnumerable<object> ScanForMiddlewares(RestClientConfiguration configuration, IValueResolver valueResolver)
        {
            var supportedTypes = new[]
            {
                typeof(RestCallMiddleware),
                typeof(RestCallMiddleware<TClient>),
                typeof(AsyncRestCallMiddleware),
                typeof(AsyncRestCallMiddleware<TClient>)
            };

            var middlewares = configuration.Middlewares.Where(x => ImplementOneOf(x.GetType(), supportedTypes)).ToList();

            middlewares.AddRange(from type in configuration.MiddlewareTypes
                                 where ImplementOneOf(type, supportedTypes)
                                 select valueResolver.Resolve(type));

            var typesFromAssemblies = configuration.AssembliesToScan
                .Where(x => x.IsDynamic == false)
                .SelectMany(x => x.GetTypes().Where(type => !type.IsAbstract && ImplementOneOf(type, supportedTypes)));

            middlewares.AddRange(from type in typesFromAssemblies select valueResolver.Resolve(type));

            return middlewares;
        }

        private static bool ImplementOneOf(Type type, IEnumerable<Type> implementTypes)
        {
            if (type?.BaseType?.IsGenericType ?? false)
                return implementTypes.Any(x => x.IsGenericType && x.IsAssignableFrom(type));
            return implementTypes.Any(x => x.IsAssignableFrom(type));
        }
    }
}
