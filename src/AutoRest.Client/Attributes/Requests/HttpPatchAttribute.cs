using RestSharp;

namespace AutoRest.Client.Attributes.Requests
{
    public class HttpPatchAttribute: HttpMethodAttribute
    {
        public HttpPatchAttribute(string template = default) : base(Method.PATCH, template)
        {
        }
    }
}