using System.Threading.Tasks;
using AutoRest.Client.Attributes;
using AutoRest.Client.Attributes.Requests;
using AutoRest.Client.Attributes.Response;
using AutoRest.Client.Tests.HttpClients.HttpBin.Models;

namespace AutoRest.Client.Tests.HttpClients.HttpBin
{
    [AddHeader("x-header-3", "x-header-value")]
    [Route("anything")]
    public interface IHttpBinAnythingClient
    {
        [HttpGet]
        [FromBody]
        Task<AnythingResponseBody> GetBodyAsync();
        
        [HttpPost("foo")]
        [AddHeader("x-header-1", "x-header-value")]
        [FromBody]
        Task<AnythingResponseBody> PostWithBindingsAsync(
            [ToQuery("queryParam", true)] string queryValue,
            [ToHeader("x-header-2")] string headerValue);

        [HttpPost]
        [FromResponse]
        Task<AnythingResponse> PostJson([ToRequest] AnythingRequest request);
        
        [HttpGet]
        [FromBody]
        AnythingResponseBody GetBodySync();
        
        [HttpGet]
        [FromRawBody]
        Task<byte[]> GetBytesBodyAsync();

        [HttpGet]
        [FromHeader("content-type")]
        Task<string> GetHeaderAsync();

        [HttpGet("{path}")]
        [FromResponse]
        Task<AnythingResponse> GetParametrizedResponse([ToPath] string path);
        
        IAnythingEndpoint AnythingEndpoint { get; set; }
    }
}