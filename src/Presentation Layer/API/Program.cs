using API.Helpers.EndpointDefinitionsHelpers;
using Infrastructure.DependencyResolver;

#region Variables

// edit variables here to run under different configs
var mainDB = "localhost";
var logDB = "localhostLogging";
//var mainDB = "DockerLocalhostDB";
//var logDB = "LoggingDockerLocalhostDB";

var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    //.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var configuration= configurationBuilder.Build();

var logConnectionString = configuration[$"ConnectionStrings:{logDB}"];

configuration[$"Serilog:WriteTo:7:Args:configureLogger:WriteTo:0:Args:connectionString"] = logConnectionString;
configuration[$"Serilog:WriteTo:8:Args:configureLogger:WriteTo:0:Args:connectionString"] = logConnectionString;
configuration[$"Serilog:WriteTo:9:Args:configureLogger:WriteTo:0:Args:connectionString"] = logConnectionString;
configuration[$"Serilog:WriteTo:10:Args:configureLogger:WriteTo:0:Args:connectionString"] = logConnectionString;
//configuration[$"Serilog:WriteTo:7:Args:configureLogger:WriteTo:0:Args:connectionString"] = logConnectionString;
//configuration[$"Serilog:WriteTo[]"] = bla;

// i want to save the changes to appsettings.json and then use the new file for the web app


#endregion

var builder = WebApplication.CreateBuilder(args);

#region Serilog

// gets its special section because it needs to sit on top of Host
builder.AddLoggerConfig(configuration, logDB);

#endregion

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region MINE

// add all infrastructure layer
builder.Services.AddInfrastructure(builder.Configuration, mainDB);

// add all endpoints which implement IEndpointDefinition
builder.Services.AddEndpointDefinitions(typeof(IEndpointDefinition));

#endregion




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// perform the DefineEndpoints() for each class that implements IEndpointDefinition
// which in turn does the MapGet, MapPost etc..
app.UseEndpointDefinitions();
for (int i = 0; i < 10; i++)
{
    app.Logger.LogCritical("Critical");
    app.Logger.LogError("Error");
    app.Logger.LogWarning("Warning");
    app.Logger.LogInformation("Information");
    app.Logger.LogTrace("Trace");

}

app.Run();
