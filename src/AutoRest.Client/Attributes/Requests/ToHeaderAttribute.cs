using AutoRest.Client.Processing.Requests;

namespace AutoRest.Client.Attributes.Requests
{
    public class ToHeaderAttribute: RequestParameterBindingAttribute
    {
        private readonly string _name;
        private readonly bool _optional;

        public ToHeaderAttribute(string name = default, bool optional = false)
        {
            _name = name;
            _optional = optional;
        }
        
        public override void Bind(RequestParameterBindingContext context)
        {
            var headerName = _name ?? context.MemberName;
            var headerValue = context.MemberValue;
            if (!_optional || _optional && headerValue != default)
                context.ExecutionContext.RestRequest.AddHeader(headerName, headerValue?.ToString() ?? "");
        }
    }
}