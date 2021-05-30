using System.Collections.Generic;
using AutoRest.Client.Attributes.Requests;

namespace AutoRest.Client.Tests.HttpClients.HttpBin.Models
{
    public class AnythingRequest
    {
        [ToHeader("x-header-1")]
        public string Header { get; set; }
        
        [ToQuery("queryParam", true)]
        public string QueryParam { get; set; }
        
        [ToJsonBody]
        public Dictionary<string, string> Body { get; set; }
    }
}