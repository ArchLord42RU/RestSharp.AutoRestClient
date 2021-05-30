using System;
using System.Threading.Tasks;
using AutoRest.Client.Processing.Requests;

namespace AutoRest.Client.Attributes.Requests
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method)]
    public abstract class RequestModifierAttribute: Attribute
    {
        public virtual void Apply(RequestExecutionContext context) { }

        public virtual async Task ApplyAsync(RequestExecutionContext context)
        {
            await Task.CompletedTask;
        }
    }
}