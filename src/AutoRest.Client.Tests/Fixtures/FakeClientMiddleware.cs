using System;
using System.Threading.Tasks;
using AutoRest.Client.Processing;
using AutoRest.Client.Tests.HttpClients.FakeClient;

namespace AutoRest.Client.Tests.Fixtures
{
    public class FakeClientMiddleware: AsyncRestCallMiddleware<IFakeHttpClient>
    {
        public static bool Called { get; set; }
        
        public override async Task InvokeAsync(ExecutionContext context, Func<ExecutionContext, Task> next)
        {
            Called = true;
            await next(context);
        }
    }
}