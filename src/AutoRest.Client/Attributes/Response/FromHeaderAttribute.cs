using System;
using System.Linq;
using AutoRest.Client.Processing.Response;

namespace AutoRest.Client.Attributes.Response
{
    public class FromHeaderAttribute: ResponseParameterBindingAttribute
    {
        private readonly string _name;

        public FromHeaderAttribute(string name)
        {
            _name = name;
        }

        public override void Bind(ResponseParameterBindingContext context)
        {
            var header = context.Response?.Headers.FirstOrDefault(
                x => x.Name?.Equals(_name, StringComparison.CurrentCultureIgnoreCase) ?? false);
            
            if (header == null)
                return;

            context.ReturnValue = Convert.ChangeType(header.Value, context.ReturnValueType);
        }
    }
}