# dotnet-template-azure-iot-edge-module

[![Nuget](https://img.shields.io/nuget/v/Microsoft.Azure.IoT.Edge.Module.svg)](https://www.nuget.org/packages/Microsoft.Azure.IoT.Edge.Module/)

> dotnet template to do scaffolding for Azure IoT Edge module development.

This README will introduce how to install the dotnet template and then create Azure IoT Edge module with the template step by step.
The template will set up all necessary files for you to focus on functionality programming.

## Get Started

Make sure you have [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) installed.

Run `dotnet` command to install the template:

```bash
dotnet new install Microsoft.Azure.IoT.Edge.Module
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
Azure IoT Edge Module (C#)
Author: Microsoft

Usage:
  dotnet new aziotedgemodule [options] [template options]

Options:
  -n, --name <name>       The name for the output being created. If no name is specified, the name of the output
                          directory is used.
  -o, --output <output>   Location to place the generated output.
  --dry-run               Displays a summary of what would happen if the given command line were run if it would result
                          in a template creation.
  --force                 Forces content to be generated even if it would change existing files.
  --no-update-check       Disables checking for the template package updates when instantiating a template.
  --project <project>     The project that should be used for context evaluation.
  -lang, --language <C#>  Specifies the template language to instantiate.
  --type <project>        Specifies the template type to instantiate.

Template options:
  -s, --skipRestore              Type: bool
                                 Default: false
  -r, --repository <repository>  Type: string
                                 Default: <registry>/<repo-name>

```

```
dotnet new aziotedgemodule -o <your_module_name>
```


## Support
The team monitors the issue section on regular basis and will try to assist with troubleshooting or questions related IoT Edge tools on a best effort basis.
	
A few tips before opening an issue. Try to generalize the problem as much as possible. Examples include
- Removing 3rd party components
- Reproduce the issue with provided deployment manifest used
- Specify whether issue is reproducible on physical device or simulated device or both
Also, Consider consulting on the [docker docs channel](https://github.com/docker/docker.github.io) for general docker questions.
