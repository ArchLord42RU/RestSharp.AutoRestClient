using System;
using AutoRest.Client.Proxy;
using Castle.DynamicProxy;

namespace AutoRest.Client.Client
{
    public class AutoRestClientBuilder<TClient> where TClient : class
    {
        private Func<RestClientConfigurationProvider<TClient>> _configurationProvider;
        
        public TClient Build()
        {
            var proxy = new ProxyGenerator();

            var configurationProvider = _configurationProvider?.Invoke() ??
                                throw new InvalidOperationException("Cannot create client without configuration provider");
            
            return proxy.CreateInterfaceProxyWithoutTarget<TClient>(new IInterceptor[]
            {
                new RestSharpInterceptor<TClient>(configurationProvider)
            });
        }

        public AutoRestClientBuilder<TClient> WithConfiguration(Action<RestClientConfiguration> configurationAction)
        {
            _configurationProvider = () =>
            {
                var config = new RestClientConfiguration();
                configurationAction(config);
                return new RestClientConfigurationProvider<TClient>(config);
            };
            return this;
        }
        
        public AutoRestClientBuilder<TClient> WithConfiguration(RestClientConfigurationProvider<TClient> provider)
        {
            _configurationProvider = () => provider;
            return this;
        }
    }
}