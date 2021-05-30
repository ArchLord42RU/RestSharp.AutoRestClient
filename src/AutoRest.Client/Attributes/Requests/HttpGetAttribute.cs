using RestSharp;

namespace AutoRest.Client.Attributes.Requests
{
    public class HttpGetAttribute: HttpMethodAttribute
    {
        public HttpGetAttribute(string template = default) : base(Method.GET, template)
        {
        }
    }
}