using System.Collections.Generic;

namespace AutoRest.Client.Tests.HttpClients.HttpBin.Models
{
    public class AnythingResponseBody
    {
        public Dictionary<string, string> Args { get; set; }
        
        public Dictionary<string, string> Headers { get; set; }

        public string Method { get; set; }
        
        public string Url { get; set; }
        
        public Dictionary<string, string> Json { get; set; }
    }
}