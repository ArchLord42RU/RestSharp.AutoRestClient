using System.Threading.Tasks;
using AutoRest.Client.Tests.Fixtures;
using NUnit.Framework;

namespace AutoRest.Client.Tests
{
    public class EndpointTests
    {
        [Test]
        public async Task Should_Access_To_Endpoint()
        {
            var endpoint = HttpClientFixture.GetHttpBinClient().AnythingEndpoint;

            var response = await endpoint.GetResponseAsync();
            
            Assert.True(response.Body.Url.EndsWith("anything/endpoint"));
        }
    }
}