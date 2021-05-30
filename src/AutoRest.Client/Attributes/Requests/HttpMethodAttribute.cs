using System;
using AutoRest.Client.Processing;
using AutoRest.Client.Processing.Requests;
using RestSharp;

namespace AutoRest.Client.Attributes.Requests
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpMethodAttribute: RequestModifierAttribute
    {
        private readonly Method _method;
        private readonly string _template;

        // ReSharper disable once MemberCanBeProtected.Global
        public HttpMethodAttribute(Method method, string template = default)
        {
            _method = method;
            _template = template;
        }
        
        public override void Apply(RequestExecutionContext context)
        {
            context.RestRequest.Method = _method;
            context.RestRequest.Resource = ProcessingUtils.CombineUrl(context.RestRequest.Resource, _template);
        }
    }
}