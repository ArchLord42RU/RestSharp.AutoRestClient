using RestSharp;

namespace AutoRest.Client.Attributes.Requests
{
    public class HttpPostAttribute: HttpMethodAttribute
    {
        public HttpPostAttribute(string template = default) : base(Method.POST, template)
        {
        }
    }
}