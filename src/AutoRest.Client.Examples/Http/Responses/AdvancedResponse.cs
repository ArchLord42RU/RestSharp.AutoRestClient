using AutoRest.Client.Attributes.Response;

namespace AutoRest.Client.Examples.Http.Responses
{
    public class AdvancedResponse
    {
        [FromBody]
        public AnythingResponseBody Body { get; set; }
        
        [FromHeader("server")]
        public string ServerName { get; set; }
    }
}