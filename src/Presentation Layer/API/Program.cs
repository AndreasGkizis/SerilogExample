using API.Helpers;
using API.Helpers.EndpointDefinitionsHelpers;
using Infrastructure.DependencyResolver;

#region Variables

// edit variables here to run under different configs
var mainDB = "localhost";
var logDB = "localhostLogging";
//var mainDB = "DockerLocalhostDB";
//var logDB = "LoggingDockerLocalhostDB";

#endregion

// Indicate the position of the connection string in appsettings.json
// This will set the connection string for the logging Database
SettingsHelpers.AddOrUpdateAppSettingLogging($"ConnectionStrings:{logDB}");

var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(   
        "appsettings.json",
        optional: false,
        reloadOnChange: true)
    .AddEnvironmentVariables();

var configuration = configurationBuilder.Build();

var logConnectionString = configuration[$"ConnectionStrings:{logDB}"];

var builder = WebApplication.CreateBuilder(args);

#region Serilog

// gets its special section because it needs to sit on top of Host
builder.AddLoggerConfig(configuration);

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

app.Run();
