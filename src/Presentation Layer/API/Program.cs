using API.Helpers.EndpointDefinitionsHelpers;
using Domain.Models;
using Infrastructure.DependencyResolver;
using Serilog;

#region Variables

// edit variables here to run under different configs
var mainDB = "localhost";
var logDB = "localhostLogging";

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    //.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();


#endregion

var builder = WebApplication.CreateBuilder(args);

#region Serilog


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


app.Run();
