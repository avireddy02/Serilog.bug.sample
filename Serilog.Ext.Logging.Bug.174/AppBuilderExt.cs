using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
namespace Serilog.Ext.Logging.Bug._174
{
    public static class AppBuilderExt
    {
        /// <summary>
        /// log web requests and response
        /// </summary>
        public static IApplicationBuilder UseRequestResponseMiddleWare(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
             
            var loggerFactory = app.ApplicationServices.GetService<ILoggerFactory>();
            loggerFactory.AddSerilog();
            app.UseMiddleware<CorrelationMiddleware>();
            
            return app;
        }
    }
}
