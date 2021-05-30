using RestSharp;

namespace AutoRest.Client.Attributes.Requests
{
    public class HttpDeleteAttribute: HttpMethodAttribute
    {
        public HttpDeleteAttribute(string template = default) : base(Method.DELETE, template)
        {
        }
    }
}