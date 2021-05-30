using AutoRest.Client.Processing.Requests;

namespace AutoRest.Client.Attributes.Requests
{
    public class ToQueryAttribute: RequestParameterBindingAttribute
    {
        private readonly string _name;
        private readonly bool _optional;

        public ToQueryAttribute(string name = default, bool optional = false)
        {
            _name = name;
            _optional = optional;
        }

        public override void Bind(RequestParameterBindingContext context)
        {
            var queryParamName = _name ?? context.MemberName;
            var queryParamValue = context.MemberValue;
            if (!_optional || _optional && queryParamValue != default)
                context.ExecutionContext.RestRequest.AddQueryParameter(queryParamName, context.MemberValue?.ToString() ?? "");
        }
    }
}