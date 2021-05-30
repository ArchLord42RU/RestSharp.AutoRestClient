using System.Threading.Tasks;
using AutoRest.Client.Tests.Fixtures;
using AutoRest.Client.Tests.HttpClients.HttpBin;
using NUnit.Framework;

namespace AutoRest.Client.Tests
{
    public class ResponseTests
    {
        [Test]
        public async Task Should_Get_Body_Async()
        {
            var response = await GetClient().GetBodyAsync();
            
            CollectionAssert.IsNotEmpty(response?.Headers);
        }
        
        [Test]
        public void Should_Get_Body_Sync()
        {
            var response = GetClient().GetBodySync();
            
            CollectionAssert.IsNotEmpty(response?.Headers);
        }
        
        [Test]
        public async Task Should_Get_Body_As_Bytes_Async()
        {
            var response = await GetClient().GetBytesBodyAsync();
            
            Assert.Greater(response?.Length ?? 0, 0);
        }
        
        [Test]
        public async Task Should_Get_Only_Header()
        {
            var contentType = await GetClient().GetHeaderAsync();
            
            Assert.AreEqual("application/json", contentType);
        }
        
        private static IHttpBinAnythingClient GetClient()
            => HttpClientFixture.GetHttpBinClient();
    }
}