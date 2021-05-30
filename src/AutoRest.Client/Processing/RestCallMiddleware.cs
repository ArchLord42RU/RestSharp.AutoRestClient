using System;
using System.Threading.Tasks;

namespace AutoRest.Client.Processing
{
    public abstract class RestCallMiddleware
    {
        public abstract void Invoke(ExecutionContext context, Action<ExecutionContext> next);
    }

    public abstract class AsyncRestCallMiddleware
    {
        public abstract Task InvokeAsync(ExecutionContext context, Func<ExecutionContext, Task> next);
    }
    
    public abstract class RestCallMiddleware<TClient>: RestCallMiddleware
    {
    }

    public abstract class AsyncRestCallMiddleware<TClient>: AsyncRestCallMiddleware
    {
    } 
}