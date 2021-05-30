using AutoRest.Client.Processing.Requests;

namespace AutoRest.Client.Attributes.Requests
{
    public class ToJsonBodyAttribute: RequestParameterBindingAttribute
    {
        public override void Bind(RequestParameterBindingContext context)
        {
            context.ExecutionContext.RestRequest.AddJsonBody(context.MemberValue);
        }
    }
}