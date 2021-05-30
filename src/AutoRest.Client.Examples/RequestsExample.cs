using System;
using System.Threading.Tasks;
using AutoRest.Client.Client;
using AutoRest.Client.Examples.Http;
using AutoRest.Client.Examples.Http.Requests;
using Newtonsoft.Json;

namespace AutoRest.Client.Examples
{
    public class RequestsExample
    {
        public static async Task SimpleUsage()
        {
            var client = GetHttpBinClient();

            var response = await client.SimpleRequestResponse(new AnythingRequest());
            
            // ReSharper disable once MethodHasAsyncOverload
            Console.WriteLine(JsonConvert.SerializeObject(response));
        }
        
        public static async Task SimpleUsageOnlyHeader()
        {
            var client = GetHttpBinClient();

            var response = await client.SimpleRequestOnlyHeader();
            
            Console.WriteLine(response);
        }

        public static async Task AdvancedMapping()
        {
            var client = GetHttpBinClient();

            // Request as single object
            var response = await client.AdvancedRequestResponse(new AdvancedAnythingRequest
            {
                Body = new AnythingRequestBody
                {
                    
                },
                MyHeader = "my-header-value",
                MyQueryParameter = "some_query_param"
            });
            
            // ReSharper disable once MethodHasAsyncOverload
            Console.WriteLine(JsonConvert.SerializeObject(response));
        }
        
        public static async Task AdvancedMapping2()
        {
            var client = GetHttpBinClient();

            // Request as method param
            var response = await client.AdvancedRequestResponse(new AnythingRequestBody(), 
                "x-header-value", 123, Guid.NewGuid());
            
            // ReSharper disable once MethodHasAsyncOverload
            Console.WriteLine(JsonConvert.SerializeObject(response));
        }
        
        // Build the client
        private static IHttpBinAnythingClient GetHttpBinClient()
        {
            var builder = new AutoRestClientBuilder<IHttpBinAnythingClient>();
            builder.WithConfiguration(configuration =>
            {
                configuration.ClientConfiguration = restClient => restClient.BaseUrl = new Uri("https://httpbin.org/");
            });

            return builder.Build();
        }
    }
}