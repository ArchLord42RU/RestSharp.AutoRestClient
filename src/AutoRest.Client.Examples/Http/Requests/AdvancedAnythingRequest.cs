using AutoRest.Client.Attributes.Requests;

namespace AutoRest.Client.Examples.Http.Requests
{
    public class AdvancedAnythingRequest
    {
        [ToJsonBody]
        public AnythingRequestBody Body { get; set; }
        
        [ToHeader("x-my-header")]
        public string MyHeader { get; set; }
        
        [ToQuery("myQueryParameter")]
        public string MyQueryParameter { get; set; }
    }
}