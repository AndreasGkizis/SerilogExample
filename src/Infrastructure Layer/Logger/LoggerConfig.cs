using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.MSSqlServer;
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
            this WebApplicationBuilder builder,
            IConfiguration configuration,
            string myConnectionString
            )
        {
            builder.Logging.ClearProviders();

            var logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .ReadFrom.Configuration(builder.Configuration)
                .WriteTo.MSSqlServer(
                    connectionString: configuration.GetSection($"ConnectionStrings:{myConnectionString}").Value,
                    sinkOptions:
                        new MSSqlServerSinkOptions { 
                            AutoCreateSqlDatabase = true,
                            AutoCreateSqlTable =true,
                            TableName = configuration.GetSection($"Serilog:DatabaseConfig:TableName").Value,
                            SchemaName = configuration.GetSection($"Serilog:DatabaseConfig:SchemaName").Value,
                        },
                    appConfiguration: configuration,
                    sinkOptionsSection: configuration.GetSection("Serilog:DatabaseConfig:SinkOptions"),
                    columnOptionsSection: 
                        configuration.GetSection("Serilog:DatabaseConfig:ColumnOptions")
                    
                )
                .CreateLogger();

            builder.Logging.AddSerilog(logger);

            // this is needed to use anywhere in the app without dependency injecting 
            Log.Logger = logger;

            return builder;
        }
    }
}
