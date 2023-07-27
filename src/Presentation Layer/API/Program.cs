using API.Helpers.EndpointDefinitionsHelpers;
using Domain.Models;
using Infrastructure.DependencyResolver;
using Serilog;

#region Variables

// edit variables here to run under different configs
var mainDB = "localhost";
var logDB = "localhostLogging";

#endregion

var builder = WebApplication.CreateBuilder(args);
#region serilog

//configure serilog 
builder.AddLoggerConfig();

#endregion



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddInfrastructure(builder.Configuration, mainDB);

///mine
///


builder.Services.AddEndpointDefinitions(typeof(IEndpointDefinition));

///mine end
///


//Log.Logger = logger;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

/// mine
/// 

app.UseEndpointDefinitions();


/// mine end
/// 


app.Run();

