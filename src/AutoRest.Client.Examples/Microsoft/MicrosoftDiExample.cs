using System;
using System.Threading.Tasks;
using AutoRest.Client.Examples.Http;
using AutoRest.Client.Examples.Http.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace AutoRest.Client.Examples.Microsoft
{
    public class MicrosoftDiExample
    {
        public static async Task Run()
        {
            // Add client to DI container and register some middlewares
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddAutoRestClient<IHttpBinAnythingClient>((serviceProvider, configuration) =>
            {
                configuration.ClientConfiguration = restClient => restClient.BaseUrl = new Uri("https://httpbin.org/");
                configuration.AddMiddleware(typeof(RestClientLogger));
            });

            // Build service provider and http client instances
            var provider = serviceCollection.BuildServiceProvider();
            var client = provider.GetRequiredService<IHttpBinAnythingClient>();

            // Make requests
            var result = await client.SimpleRequestResponse(new AnythingRequest());
        }
    }
}