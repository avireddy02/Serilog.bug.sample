using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog.Events;
using Serilog.Extensions.Logging;

namespace Serilog.Ext.Logging.Bug._174
{
    public class Program
    {
         
        public static void Main(string[] args)
        {
            var providers = new LoggerProviderCollection();
            var seriConfig = new LoggerConfiguration()
                       .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                       .MinimumLevel.Override("System", LogEventLevel.Error)
                       .MinimumLevel.Override("Serilog.Ext.Logging.Bug._174.Controllers.WeatherForecastController", LogEventLevel.Verbose)
                       .Enrich.FromLogContext() 
                      ;

            seriConfig.WriteTo.Console(
                    restrictedToMinimumLevel: LevelConvert.ToSerilogLevel(LogLevel.Information),
                    standardErrorFromLevel: LogEventLevel.Error);
            Log.Logger = seriConfig.CreateLogger();
            System.Diagnostics.Activity.DefaultIdFormat = System.Diagnostics.ActivityIdFormat.W3C;
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    }).ConfigureLogging(l => l.AddSerilog())
                    .UseSerilog();
            return hostBuilder;

     ;
        }
    }
}
