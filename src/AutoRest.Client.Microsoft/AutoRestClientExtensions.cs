using System;
using System.Linq;
using AutoRest.Client.Client;
using AutoRest.Client.Microsoft;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class AutoRestClientExtensions
    {
        /// <summary>
        /// Adds auto rest client to DI container
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="lifetime"></param>
        /// <typeparam name="TClient"></typeparam>
        /// <returns></returns>
        public static IServiceCollection AddAutoRestClient<TClient>(this IServiceCollection services, 
            Action<IServiceProvider, RestClientConfiguration> configuration, ServiceLifetime lifetime = ServiceLifetime.Transient)
            where TClient : class
        {
            if (services.Any(sd => sd.ServiceType == typeof(TClient)))
                return services;

            services.AddTransient(provider =>
            {
                var restConfig = new RestClientConfiguration();

                configuration(provider, restConfig);

                restConfig.ValueResolver ??= new ServiceProviderValueResolver(provider);
                
                return new RestClientConfigurationProvider<TClient>(restConfig);
            });

            var descriptor = new ServiceDescriptor(typeof(TClient), provider => new AutoRestClientBuilder<TClient>()
                .WithConfiguration(provider.GetRequiredService<RestClientConfigurationProvider<TClient>>()).Build(), lifetime);
            
            services.Add(descriptor);

            return services;
        }
    }
}