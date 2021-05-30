using System;
using AutoRest.Client.Processing.Requests;

namespace AutoRest.Client.Attributes.Requests
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class RouteAttribute: RequestModifierAttribute
    {
        private readonly string _template;

        public RouteAttribute(string template)
        {
            _template = template;
        }
        
        public override void Apply(RequestExecutionContext context)
        {
            context.RestRequest.Resource = CombineResources(context.RestRequest.Resource, _template);
        }

        private static string CombineResources(string left, string right)
        {
            if (string.IsNullOrEmpty(left))
                return right;
            
            if (string.IsNullOrEmpty(right))
                return left;
            
            return new Uri(new Uri(left), right).ToString();
        }
    }
}