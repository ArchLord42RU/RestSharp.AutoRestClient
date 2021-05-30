using AutoRest.Client.Attributes.Response;

namespace AutoRest.Client.Tests.HttpClients.HttpBin.Models
{
    public class AnythingResponse
    {
        [FromBody]
        public AnythingResponseBody Body { get; set; }
        
        [FromHeader("server")]
        public string Server { get; set; }
    }
}