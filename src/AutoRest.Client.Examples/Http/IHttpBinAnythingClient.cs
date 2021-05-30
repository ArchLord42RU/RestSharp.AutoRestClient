using System;
using System.Threading.Tasks;
using AutoRest.Client.Attributes.Requests;
using AutoRest.Client.Attributes.Response;
using AutoRest.Client.Examples.Http.Requests;
using AutoRest.Client.Examples.Http.Responses;
using RestSharp;

namespace AutoRest.Client.Examples.Http
{
    // Example of rest client
    public interface IHttpBinAnythingClient
    {
        //Simple requests that receive only response body / header(s) and etc
        
        [HttpGet]
        [FromBody]
        Task<AnythingResponseBody> SimpleRequestResponse(AnythingRequest request);
        
        [HttpMethod(Method.OPTIONS)]
        [FromHeader("server")]
        Task<string> SimpleRequestOnlyHeader();

        //Advanced requests that receive body, headers and other mapped values
        //And map response in several different ways
        
        [HttpPost]
        Task<AdvancedResponse> AdvancedRequestResponse([ToRequest] AdvancedAnythingRequest anythingRequest);
        
        [HttpPost("{pathParam}")] //Describe path param in method parameters
        [FromBody]
        Task<AnythingResponseBody> AdvancedRequestResponse(
            [ToJsonBody] AnythingRequestBody body,
            [ToHeader("x-my-header")] string myHeader,
            [ToQuery("myQueryParameter")] int myQueryParam,
            [ToPath("pathParam")] Guid myPathParam);
    }
}