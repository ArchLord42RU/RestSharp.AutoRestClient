using AutoRest.Client.Processing.Requests;

namespace AutoRest.Client.Attributes.Requests
{
    public class ToPathAttribute: RequestParameterBindingAttribute
    {
        private readonly string _paramName;

        public ToPathAttribute(string paramName = default)
        {
            _paramName = paramName;
        }
        
        public override void Bind(RequestParameterBindingContext context)
        {
            var paramName = _paramName ?? context.MemberName;
            
            context.ExecutionContext.RestRequest.AddUrlSegment(paramName, context.MemberValue);
        }
    }
}