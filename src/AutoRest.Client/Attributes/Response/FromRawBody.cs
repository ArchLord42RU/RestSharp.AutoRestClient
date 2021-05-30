using AutoRest.Client.Processing.Response;

namespace AutoRest.Client.Attributes.Response
{
    public class FromRawBody: ResponseParameterBindingAttribute
    {
        public override void Bind(ResponseParameterBindingContext context)
        {
            context.ReturnValue = context.Response.RawBytes;
        }
    }
}