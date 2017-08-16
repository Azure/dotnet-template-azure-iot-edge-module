# dotnet-template-azure-iot-edge-module
> dotnet template to do scaffolding tool for azure iot edge module development.

This ReadMe consists of two parts:
- Get Started to introduce how to install the dotnet template nuget package step by step
- Containerize the module

  The dotnet template sets up all necessary files for you to focus on module functionality programming.

  After the coding part completed, following the steps in this part to leverage docker to containerize your module so that they can be deployed and monitored by the new features of Azure IoT Edge more straight forward.

## Get Started

Make sure you have [Nuget](https://www.nuget.org/) installed.

Run following command to add the template nuget source:

- For NeGet V3
```
nuget sources add -name AzureIoTEdgeModuleGenerator -source https://www.myget.org/F/dotnet-template-azure-iot-edge-module/api/v3/index.json
```

- For NuGet V2

```
nuget sources add -name AzureIoTEdgeModuleGenerator -source https://www.myget.org/F/dotnet-template-azure-iot-edge-module/api/v2
```

Check the nuget source is added successfully and enabled by executing command **nuget sources**, check the output:

```
Registered Sources:

  1.  nuget.org [Enabled]
      https://api.nuget.org/v3/index.json
  2.  https://www.nuget.org/api/v2/ [Disabled]
      https://www.nuget.org/api/v2/
  3.  AzureIoTEdgeModuleGenerator [Enabled]
      https://www.myget.org/F/dotnet-template-azure-iot-edge-module/api/v2
```

Install the nuget package:
```
nuget install Azure.IoT.Edge.Module.Generator
```
You will get a new folder with name *Azure.IoT.Edge.Module.Generator.0.0.1*, check out the content in this folder, run dotnet command to install the template with correct path:

```
dotnet new -i <.\Azure.IoT.Edge.Module.Generator.0.0.1\content\dotnet-template-azure-iot-edge-module\CSharp\>
```
You could find our template with short name *aziotedgemodulegen* in the output:

```
Templates                                         Short Name              Language          Tags
---------------------------------------------------------------------------------------------------------------
Console Application                               console                 [C#], F#, VB      Common/Console
Class library                                     classlib                [C#], F#, VB      Common/Library
Azure IoT Edge Module Generator                   aziotedgemodulegen      [C#]              Console
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

Check out all arguments the template may take:
```
dotnet net aziotedgemodulegen --help
```

It will output a full list as following:

```
Azure IoT Edge Module Generator (C#)
Author: Summer Sun
Options:
  -mo|--moduleName
                      string - Optional
                      Default: SampleModule

  -m|--moduleType
                          CM     - Custom Module
                          AF     - Azure Function
                          AML    - Azure Machine Learning
                          ASA    - Azure Stream Analytics
                      Default: CM

  -s|--skipRestore
                      bool - Optional
                      Default: false

  -wx|--windows-x64
                      bool - Optional
                      Default: true

  -wix|--windows-x86
                      bool - Optional
                      Default: false

  -lx|--linux-x64
                      bool - Optional
                      Default: false

  -lix|--linux-x86
                      bool - Optional
                      Default: false

  -ax|--arm-x86
                      bool - Optional
                      Default: false
```

Now create the azure iot edge module by the template with arguments you want:

```
dotnet new aziotedgemodulegen -n <ProjectName> -mo <ModuleName> -m CM -wx true -lx true
```
We support multiple architectures, so users have to specify all the architectures corresponding arguments to true to enable it. windows-x64 is default true.
You could refer to above list for argument meaning.

Now all set up files to develop an azure iot edge module are generated.

## Containerize the module

azure-iot-edge-module sets up the Azure IoT Edge Module development environment, generating all necessary files for you.

To containerize the module, there are several extra steps to do.

### Install Docker 
Ubuntu

https://docs.docker.com/engine/installation/linux/docker-ce/ubuntu/

Windows 10

https://download.docker.com/win/stable/InstallDocker.msi

MAC

https://store.docker.com/editions/community/docker-ce-desktop-mac

Now navigate to the generated module folder in the first place.

### Build your module
```
dotnet build
dotnet publish -f netcoreapp2
```
### Create and run local docker registry
```
docker run -d -p 5000:5000 --name registry resigtry:2
```
### Build docker image for your module
```
docker build --build-arg EXE_DIR=./bin/Debug/netcoreapp2/publish -t localhost:5000/<lower_case_module_name>:latest <docker_file_directory>
```
### Push the image to local registry
```
docker push localhost:5000/<lower_case_module_name>
```
