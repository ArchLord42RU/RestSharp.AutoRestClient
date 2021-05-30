using System;
using AutoRest.Client.Processing;
using Microsoft.Extensions.Logging;

namespace AutoRest.Client.Examples.Microsoft
{
    public class RestClientLogger: RestCallMiddleware
    {
        private readonly ILogger _logger;

        public RestClientLogger(ILogger logger)
        {
            _logger = logger;
        }

        public override void Invoke(ExecutionContext context, Action<ExecutionContext> next)
        {
            using var scope = _logger.BeginScope(context.ClientType);
            _logger.LogInformation(context.Request.Resource);
            next(context);
        }
    }
}