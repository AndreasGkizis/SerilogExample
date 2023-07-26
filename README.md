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

