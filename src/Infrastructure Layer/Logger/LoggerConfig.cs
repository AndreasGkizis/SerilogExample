using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
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
                .MinimumLevel.Information()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Logging.AddSerilog(logger);

            // this is needed to use anywhere in the app without dependency injecting 
            Log.Logger = logger;

            return builder;
        }
    }
}
