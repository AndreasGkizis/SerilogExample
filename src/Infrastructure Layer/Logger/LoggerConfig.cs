using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Serilog;

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

            return builder;
        }
    }
}
