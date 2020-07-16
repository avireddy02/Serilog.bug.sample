using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System.Threading.Tasks;

namespace Serilog.Ext.Logging.Bug._174
{
    internal class CorrelationMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// method called from the middleware pipeline
        /// </summary>
        public async Task Invoke(HttpContext httpContext)
        {

            var traceId = System.Diagnostics.Activity.Current.TraceId.ToString();

            LogContext.PushProperty("X-Corr-Id", traceId);
            await _next.Invoke(httpContext);
        }
    }
}