using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public static class LoggerConfig
    {
        public static WebApplicationBuilder AddLoggerConfiguration(
            this WebApplicationBuilder builder
            )
        {
            builder.Logging.ClearProviders();

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();


            //Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

            builder.Host.UseSerilog(logger);

            // this is needed to use anywhere in the app without dependency injecting 
            Log.Logger = logger;

            return builder;
        }
    }
}
