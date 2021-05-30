using System;
using AutoRest.Client.Client;
using AutoRest.Client.Tests.HttpClients.HttpBin;

namespace AutoRest.Client.Tests.Fixtures
{
    public static class HttpClientFixture
    {
        public const string QueryParamValue = "query-param-value";
        public const string HeaderParamValue = "x-header-value";
        
        public static IHttpBinAnythingClient GetHttpBinClient(Action<RestClientConfiguration> setupAction = default)
        {
            var builder = new AutoRestClientBuilder<IHttpBinAnythingClient>()
                .WithConfiguration(configuration =>
                {
                    configuration.ClientConfiguration = client =>
                    {
                        client.BaseUrl = new Uri("https://httpbin.org");
                    };
                    setupAction?.Invoke(configuration);
                });

            return builder.Build();
        }
    }
}