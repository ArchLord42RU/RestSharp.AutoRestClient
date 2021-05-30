using RestSharp;

namespace AutoRest.Client.Attributes.Requests
{
    public class HttpPutAttribute: HttpMethodAttribute
    {
        public HttpPutAttribute(string template = default) : base(Method.PUT, template)
        {
        }
    }
}