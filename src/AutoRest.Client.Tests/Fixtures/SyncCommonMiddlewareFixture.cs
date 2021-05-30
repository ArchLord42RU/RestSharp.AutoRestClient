using System;
using AutoRest.Client.Processing;

namespace AutoRest.Client.Tests.Fixtures
{
    public class SyncCommonMiddlewareFixture: RestCallMiddleware
    {
        public static bool Called { get; set; }
        
        public override void Invoke(ExecutionContext context, Action<ExecutionContext> next)
        {
            Called = true;
            next(context);
        }
    }
}