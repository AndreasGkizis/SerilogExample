# Serilog Implementation in Minimal API project (C#)
---

## Introduction

This repository demonstrates a minimal API implementation in [.NET 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) using [Serilog](https://serilog.net/) for logging. 

The goal was to learn about logging in general in order to apply in later projects 

# Table of Contents

1. [Introduction](#Introduction)
1. [How to install locally](#installation)
1. [How it works]()
2. [Lessons Learned](#lessons-learned)
3. [Getting Started](#getting-started)
4. [Future of project](#things-i-want-implement-in-the-future)
## How to install 

#### Prerequisites 
1. The [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) since the app has been developed using it
1. A version of [Microsoft SQL Server 2022](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)(Developer version) either running on your machine or remotely 

#### Steps to run  
1. Clone this repository 
```git
$ git clone https://github.com/AndreasGkizis/SerilogExample.git
```
2. Move into the directory 
```bash
$ cd SerilogExample
```
3. Get all The branches available  
```git
$ git fetch --all
```
4. Restore Dependencies for all the projects
```bash
$ dotnet restore 
```
5. Configure the Connection string to your local database in appsettings.json  
```bash
$ dotnet restore 
```
6. Run the project!  
```bash
$ dotnet run --project src/Presentation\ Layer/API/API.csproj
```

## Lessons learned  🕮
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
    
### Conclusion
Between the two methods the preferred way is to use a configuration file like appsettings.json. 
The reason for this is that the program does not have to re-run for the changes to take effect !
You can simply change something in the file and the changes are applied immediately to your logger (pretty cool if you ask me!).
In production this has the obvious plus of zero downtime for any changes so this is the way I aimed to set up my configuration

## &#127937; Getting Started 

There are Several branches to this repository 
1. [simple_debug_console_file_logging](https://github.com/AndreasGkizis/SerilogExample/tree/simple_debug_console_file_logging)

    Has simple logs for writing in the Console, the Debug Console and a file in the program.cs level
1. [ using_appsettings.json_for_serilog]()


    Has Configuration for separate files depending on the log level and also uses MSSQL
to log in tables according to the log level making it easier to query them if needed


1. [dynamicappsettings]()

    Dynamically edit the appsettings.json file vie the program.cs file before creating the logger.
This way the user of the program would need to enter the connection string only once and it would populate the file
and use the new file to configure the loggers


## Things I want to implement in the future  :crystal_ball:
1. [ ] Docker Support for both the app and the database
1. [ ] Improve the recursive functions to be more adaptive
1. [ ] Add summaries to all functions



### &#127882; Any Feedback is always welcome &#128170;

&#10097; Feel free to contact me via my [LinkedIn Profile](https://www.linkedin.com/in/andreas-gkizis-a9ab29a5/)