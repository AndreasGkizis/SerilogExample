# Serilog Implementation in Minimal API project (C#)
---

### Introduction

This repository demonstrates a minimal API implementation in .NET 7  
using Serilog for logging. The goal was to learn about logging in general since it is important to have a logger in place
before proceeding to develop the full product in order to be able to watch over the application's behavior 

## Project Goals

Configure the Logger in many ways and with a logical separation which would be helpful when attempting to read through the logs 

## Lessons learned ! 
There are two main ways to configure any logging framework 
 
#### <u> Classes to set the configuration as needed </u>
###### **PROS +**
1. Easier to set up due to the abundance of available classes which cover almost any needs
1. There is more information online on how to configure Serilog in this way 
###### **CONS -** 
1. The application needs to be re-run in order for the changes to take effect 
#### <u> Using a file like appsettings.json (or any other file) to set the configuration</u>
###### **PROS +**
1. The application does not need to restart in order for changes to be applied

###### **CONS -** 
1. Repetitive entries in the file to configure many Sinks (might be able to fix that) 

1. Less information and examples online
    
Between the two methods the prefered way is to use a configuration file like appsettings.json. 
The reason for this is that the program does not have to re-run for the changes to take effect !
You can simply change something in the file and the changes are applied immediately to your logger (pretty cool if you ask me!).
In production this has the obvious plus of zero downtime for any changes so this is the way I aimed to set up my configuration

## Getting Started

There are Several branches to this repository 
1. [simple_debug_console_file_logging](https://github.com/AndreasGkizis/SerilogExample/tree/simple_debug_console_file_logging)

    Has simple logs for writing in the Console, the Debug Console and a file in the program.cs level
1. [placehoder_name]()


    Has Configuration for separate files depending on the log level and also uses MSSQL
to log in tebales according to the log level making it easier to query them if needed


1. [simple_debug_console_file_logging]()

    In this case I am attempting to dynamically edit the appsettings.json file before creating the logger.
This way the user of the program would need to enter the connection string only once and it would populate the file
























Follow the steps below to get the project up and running on your local machine:

1. Clone the repository:

```bash
git clone https://github.com/your-username/your-repo.git

    Navigate to the project directory:

bash

cd your-repo

    Build the project:

bash

dotnet build

    Run the application:

bash

dotnet run

The API will be hosted at http://localhost:5000.
Logging with Serilog

This project uses Serilog as the logging library to capture application logs. The logging configuration is set up in the Program.cs file.
Configuration

In the Program.cs file, the ConfigureLogging method is used to set up Serilog with a simple console sink:

csharp

using Serilog;
using Serilog.Events;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        var builder = WebApplication.CreateBuilder(args);
        // Other application configurations...
        // ...
        // ...
        builder.Build().Run();
    }
}

In this example, we set the minimum log level to Warning for Microsoft loggers, and the logs are written to the console. You can customize the Serilog configuration as per your requirements, such as adding additional sinks, specifying log file paths, etc.
Endpoints

The API exposes the following endpoints:

    GET /api/hello: Returns a simple "Hello, World!" message.

Feel free to extend this API with additional endpoints and business logic as needed.
Contributing

If you find any issues or have suggestions for improvements, feel free to open an issue or submit a pull request.
License

This project is licensed under the MIT License.

csharp


Please note that this is just a basic example README.md file to guide users abou