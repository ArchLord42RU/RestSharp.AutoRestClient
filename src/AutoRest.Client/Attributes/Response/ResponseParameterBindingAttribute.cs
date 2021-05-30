using System;
using System.Threading.Tasks;
using AutoRest.Client.Processing.Response;

namespace AutoRest.Client.Attributes.Response
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public abstract class ResponseParameterBindingAttribute: Attribute
    {
        public virtual void Bind(ResponseParameterBindingContext context) { }

        public virtual async Task BindAsync(ResponseParameterBindingContext context)
        {
            await Task.CompletedTask;
        }
    }
}