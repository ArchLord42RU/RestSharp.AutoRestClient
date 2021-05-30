using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoRest.Client.Processing;
using AutoRest.Client.Processing.Requests;

namespace AutoRest.Client.Attributes.Requests
{
    public class ToRequestAttribute: RequestParameterBindingAttribute
    {
        public override async Task BindAsync(RequestParameterBindingContext context)
        {
            await ProcessingUtils.ApplyRequestModifiersAttributes(context.MemberType, context.ExecutionContext);

            var props = context.MemberType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.CanRead);

            foreach (var prop in props)
            {
                var propContext = new RequestParameterBindingContext
                {
                    ExecutionContext = context.ExecutionContext,
                    MemberAttributes = prop,
                    MemberName = prop.Name,
                    MemberType = prop.PropertyType,
                    MemberValue = prop.GetValue(context.MemberValue)
                };
                await ProcessingUtils.ApplyRequestParameterBindingAttributes(prop, propContext);    
            }
        }
    }
}