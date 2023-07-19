using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
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
            var environment = configuration.GetValue<string>("Environment");
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            // clears any preexisting ILoggerProvider 
            builder.Logging.ClearProviders();

            // customise the loggers used
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.WithProperty("Environment", env)
                // create conditional write to so that it writes every level to another table 
                .WriteTo.Conditional(
                    ev => ev.Level <= LogEventLevel.Fatal,
                    wt => wt.MSSqlServer(
                    connectionString: configuration.GetSection($"ConnectionStrings:{myConnectionString}").Value,
                    sinkOptions:
                        new MSSqlServerSinkOptions
                        {
                            // this is the MINIMUM LEVEL
                            LevelSwitch = new LoggingLevelSwitch(LogEventLevel.Fatal),
                            AutoCreateSqlDatabase = true,
                            AutoCreateSqlTable = true,
                            TableName = "FatalLogs"
                        },
                    appConfiguration: configuration,
                    sinkOptionsSection: configuration.GetSection("Serilog:DatabaseConfig:SinkOptionsSection"),
                    columnOptionsSection:
                        configuration.GetSection("Serilog:DatabaseConfig:ColumnOptions")
                        )
                ).WriteTo.Conditional(
                    ev => ev.Level <= LogEventLevel.Error,
                    wt => wt.MSSqlServer(
                    connectionString: configuration.GetSection($"ConnectionStrings:{myConnectionString}").Value,
                    sinkOptions:
                        new MSSqlServerSinkOptions
                        {
                            // this is the MINIMUM LEVEL
                            LevelSwitch = new LoggingLevelSwitch(LogEventLevel.Error),
                            AutoCreateSqlDatabase = true,
                            AutoCreateSqlTable = true,
                            TableName = "ErrorLogs"
                        },
                    appConfiguration: configuration,
                    sinkOptionsSection: configuration.GetSection("Serilog:DatabaseConfig:SinkOptionsSection"),
                    columnOptionsSection:
                        configuration.GetSection("Serilog:DatabaseConfig:ColumnOptions")
                )
                ).WriteTo.Conditional(
                    ev => ev.Level <= LogEventLevel.Warning,
                    wt => wt.MSSqlServer(
                    connectionString: configuration.GetSection($"ConnectionStrings:{myConnectionString}").Value,
                    sinkOptions:
                        new MSSqlServerSinkOptions
                        {
                            // this is the MINIMUM LEVEL
                            LevelSwitch = new LoggingLevelSwitch(LogEventLevel.Warning),
                            AutoCreateSqlDatabase = true,
                            AutoCreateSqlTable = true,
                            TableName = "WarningLogs"
                        },
                    appConfiguration: configuration,
                    sinkOptionsSection: configuration.GetSection("Serilog:DatabaseConfig:SinkOptionsSection"),
                    columnOptionsSection:
                        configuration.GetSection("Serilog:DatabaseConfig:ColumnOptions")
                )
                ).WriteTo.Conditional(
                    ev => ev.Level <= LogEventLevel.Information,
                    wt => wt.MSSqlServer(
                    connectionString: configuration.GetSection($"ConnectionStrings:{myConnectionString}").Value,
                    sinkOptions:
                        new MSSqlServerSinkOptions
                        {
                            // this is the MINIMUM LEVEL
                            LevelSwitch = new LoggingLevelSwitch(LogEventLevel.Information),
                            AutoCreateSqlDatabase = true,
                            AutoCreateSqlTable = true,
                            TableName = "InformationLogs"
                        },
                    appConfiguration: configuration,
                    sinkOptionsSection: configuration.GetSection("Serilog:DatabaseConfig:SinkOptionsSection"),
                    columnOptionsSection:
                        configuration.GetSection("Serilog:DatabaseConfig:ColumnOptions")
                )
                )

                .CreateLogger();

            //builder.Logging.AddSerilog(logger);

            builder.Host.UseSerilog(logger);

            // this is needed to use anywhere in the app without dependency injecting 
            Log.Logger = logger;

            return builder;
        }
    }
}
