using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoRest.Client.Tests.Asserts;
using AutoRest.Client.Tests.Fixtures;
using AutoRest.Client.Tests.HttpClients.HttpBin;
using AutoRest.Client.Tests.HttpClients.HttpBin.Models;
using NUnit.Framework;

namespace AutoRest.Client.Tests
{
    public class RequestTests
    {
        [Test]
        public async Task Should_Check_Params_Binding()
        {
            const string queryParam = HttpClientFixture.QueryParamValue;
            const string headerParam = HttpClientFixture.HeaderParamValue;
            
            var response = await GetClient().PostWithBindingsAsync(queryParam, headerParam);
            
            Assert.AreEqual("POST", response.Method);

            CustomAsserts.DictionaryContainsKeyAndValue(response.Args, "queryParam", queryParam);
            CustomAsserts.DictionaryContainsKeyAndValue(response.Headers, "X-Header-1", headerParam);
            CustomAsserts.DictionaryContainsKeyAndValue(response.Headers, "X-Header-2", headerParam);
            CustomAsserts.DictionaryContainsKeyAndValue(response.Headers, "X-Header-3", headerParam);
    
            Assert.True(response.Url.Contains("/foo"));
        }
        
        [Test]
        public async Task Should_Check_Optional_Params_Binding()
        {
            const string headerParam = HttpClientFixture.HeaderParamValue;
            
            var response = await GetClient().PostWithBindingsAsync(default, headerParam);

            CustomAsserts.DictionaryDoesNotContainsKey(response.Args, "queryParam");
        }
        
        [Test]
        public async Task Should_Post_Bound_Request()
        {
            const string queryParam = HttpClientFixture.QueryParamValue;
            const string headerParam = HttpClientFixture.HeaderParamValue;
            
            var req = new AnythingRequest
            {
                Header = headerParam,
                QueryParam = queryParam,
                Body = new Dictionary<string, string>
                {
                    {"key", "value"}
                }
            };

            var response = await GetClient().PostJson(req);
            
            CustomAsserts.DictionaryContainsKeyAndValue(response.Body.Args, "queryParam", queryParam);
            CustomAsserts.DictionaryContainsKeyAndValue(response.Body.Headers, "X-Header-1", headerParam);
            CustomAsserts.DictionaryContainsKeyAndValue(response.Body.Json, "key", "value");
            Assert.AreEqual(false, string.IsNullOrEmpty(response.Server));
        }

        [Test]
        public async Task Should_Send_Parametrized_Path_Request()
        {
            var path = Guid.NewGuid().ToString();
            var response = await GetClient().GetParametrizedResponse(path);

            Assert.True(response.Body.Url.EndsWith(path));
        }

        private static IHttpBinAnythingClient GetClient()
            => HttpClientFixture.GetHttpBinClient();
    }
}