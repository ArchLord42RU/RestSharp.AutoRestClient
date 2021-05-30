using System;
using AutoRest.Client.Processing;
using Microsoft.Extensions.DependencyInjection;

namespace AutoRest.Client.Microsoft
{
    public class ServiceProviderValueResolver: IValueResolver
    {
        private readonly IServiceProvider _provider;

        public ServiceProviderValueResolver(IServiceProvider provider)
        {
            _provider = provider;
        }

        public object Resolve(Type objectType)
        {
            return ActivatorUtilities.CreateInstance(_provider, objectType);
        }
    }
}