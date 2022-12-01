# dotnet-template-azure-iot-edge-module

[![Nuget](https://img.shields.io/nuget/v/Microsoft.Azure.IoT.Edge.Module.svg)](https://www.nuget.org/packages/Microsoft.Azure.IoT.Edge.Module/)

> dotnet template to do scaffolding for Azure IoT Edge module development.

This README will introduce how to install the dotnet template and then create Azure IoT Edge module with the template step by step.
The template will set up all necessary files for you to focus on functionality programming.

## Get Started

Make sure you have [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) installed.

Run `dotnet` command to install the template:

```
dotnet new -i Microsoft.Azure.IoT.Edge.Module
```
You could find the template with short name *aziotedgemodule* in output:

```
Templates                                         Short Name              Language          Tags
---------------------------------------------------------------------------------------------------------------
Console Application                               console                 [C#], F#, VB      Common/Console
Class library                                     classlib                [C#], F#, VB      Common/Library
Azure IoT Edge Module                             aziotedgemodule         [C#]              Console
Contoso Sample 06                                 sample06                [C#], F#          Console
Unit Test Project                                 mstest                  [C#], F#, VB      Test/MSTest
xUnit Test Project                                xunit                   [C#], F#, VB      Test/xUnit
ASP.NET Core Empty                                web                     [C#]              Web/Empty
ASP.NET Core Web App (Model-View-Controller)      mvc                     [C#], F#          Web/MVC
ASP.NET Core Web App (Razor Pages)                razor                   [C#]              Web/MVC/Razor Pages
ASP.NET Core with Angular                         angular                 [C#]              Web/MVC/SPA
ASP.NET Core with React.js                        react                   [C#]              Web/MVC/SPA
ASP.NET Core with React.js and Redux              reactredux              [C#]              Web/MVC/SPA
ASP.NET Core Web API                              webapi                  [C#]              Web/WebAPI
Nuget Config                                      nugetconfig                               Config
Web Config                                        webconfig                                 Config
Solution File                                     sln                                       Solution
Razor Page                                        page                                      Web/ASP.NET
MVC ViewImports                                   viewimports                               Web/ASP.NET
MVC ViewStart                                     viewstart                                 Web/ASP.NET
```

Check out the template details:
```
PS C:\> dotnet new aziotedgemodule --help
Usage: new [options]

Options:
  -h, --help          Displays help for this command.
  -l, --list          Lists templates containing the specified name. If no name is specified, lists all templates.
  -n, --name          The name for the output being created. If no name is specified, the name of the current directory is used.
  -o, --output        Location to place the generated output.
  -i, --install       Installs a source or a template pack.
  -u, --uninstall     Uninstalls a source or a template pack.
  --type              Filters templates based on available types. Predefined values are "project", "item" or "other".
  --force             Forces content to be generated even if it would change existing files.
  -lang, --language   Specifies the language of the template to create.


Azure IoT Edge Module (C#)
Author: Microsoft
Options:
  -s|--skipRestore
                      bool - Optional
                      Default: false
  
  -r|--repository
                      string - Optional
                      Default: <registry>/<repo-name> 

  -lang|--language
                      string - Optional
                      Default: C#

```

Parameter `-s` means if you want to skip the restore of packages referenced in module project.

Parameter `-r` means the Docker repository to host your Azure IoT Edge module.

Now create the Azure IoT Edge module by the template with name:

```
dotnet new aziotedgemodule -n <your_module_name>
```

Optionally, to create an F# module use the `-lang` or `--language` flag as follows:

```
dotnet new aziotedgemodule -lang F# -n <your_module_name>
``` 

## Support
The team monitors the issue section on regular basis and will try to assist with troubleshooting or questions related IoT Edge tools on a best effort basis.
	
A few tips before opening an issue. Try to generalize the problem as much as possible. Examples include
- Removing 3rd party components
- Reproduce the issue with provided deployment manifest used
- Specify whether issue is reproducible on physical device or simulated device or both
Also, Consider consulting on the [docker docs channel](https://github.com/docker/docker.github.io) for general docker questions.
