
# Debug,Console,file and Microsoft SQL Server Implementation.
---
## Summary
On this branch I use Serilog in order to configure a logger which writes to the Debug,Console,file and Microsoft SQL Server.
All the settings for the above are in the `appsettings.json` file and are retrieved from there. There are seperate files and tables created for every log level.
## How does it work ??

Here is the logic flow of this branch and how to use it

1. In Program.cs we invoke the `ConfigurationBuilder()` in order to customize how the configuration is loaded in our application
	1. we Set the path where the `appsettings.json` is located
	1. And we add the specified file , important to note here is that we force a reload when anything on this file is changed with indicating `reloadOnChange: true`.
	This allows us to leverage the immediate change in logger configuration when anything is changed in the `appsettings.json` file.
1. We call the `AddLoggerConfig()` extension method which was created to handle all the configuration of the logger
	1. Inside we call   `builder.AddLoggerConfiguration()` which handles the configuration of the logger and afterwards `UseSerilog()` 
	which is a ready extension method by Serilog in order to all the logger to the request/response pipeline 

As far as the Logger is concerned we are done and configured !

## See it in Action!

In this there is just one endpoint provided. Fear not however this will be enough =]

Once you run the app you will see the one available endpoint 
`https://localhost:7252/user/getall`. Out of the box once you execute a call to that you will see in all Debug Console, Console and File that Logs will be created.

Since this is using the `appsettings.json` we can change levels on the go and without restarting the application see the results!
To try this edit the `appsettings.json` and change this part for any of the loggers
`"restrictedToMinimumLevel": "Information"`

The list of available Serilog logging levels :
```
Verbose
Debug
Information
Warning
Error
Fatal
```
Once you save the file and execute the request again you will see that it will change it's behavior to whatever you indicated !

