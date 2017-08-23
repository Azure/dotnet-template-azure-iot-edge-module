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

- For NuGet V3
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
You could find our template with short name *aziotedgemodule* in the output:

```
Templates                                         Short Name              Language          Tags
---------------------------------------------------------------------------------------------------------------
Console Application                               console                 [C#], F#, VB      Common/Console
Class library                                     classlib                [C#], F#, VB      Common/Library
Azure IoT Edge Module Generator                   aziotedgemodule         [C#]              Console
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
dotnet new aziotedgemodule --help
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
dotnet new aziotedgemodule -n <ProjectName> -mo <ModuleName> -m CM -wx true -lx true
```
We support multiple architectures, so users have to specify all the architectures corresponding arguments to true to enable it. windows-x64 is default true.
You could refer to above list for argument meaning.

Now all set up files to develop an azure iot edge module are generated.

## Deploy and run the module

azure-iot-edge-module sets up the Azure IoT Edge Module development environment, generating all necessary files for you.

To make the module executable, there are several steps to do.

### Install docker
Ubuntu

https://docs.docker.com/engine/installation/linux/docker-ce/ubuntu/

Windows 10

https://download.docker.com/win/stable/InstallDocker.msi

MAC

https://store.docker.com/editions/community/docker-ce-desktop-mac

Now navigate to the generated module folder in the first place.

### Setup azure resources

If you have develop experience with Azure, you could skip this part and go ahead to next one.

1. Create an active Azure account

(If you don't have an account, you can create one [free account](http://azure.microsoft.com/pricing/free-trial/) in minutes.)

2. Create an Azure IoT Hub

Reference [How to create an azure iot hub] (https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-create-through-portal) for step by step guidance.

3. Create a device in azure iot hub

Navigate to your iot hub in azure portal, find the **Device Explorer** to **Add** a device in the portal.
Mark up the device connection string after creating completed.

### Install the edge cli

```
npm install -g edge-explorer@latest --registry http://edgenpm.southcentralus.cloudapp.azure.com/
```
### Install tool to launch Azure IoT Edge

> On Windows, If you have issues on the command line with the --registry command, try to use a PowerShell session

```
npm install -g launch-edge-runtime@latest --registry http://edgenpm.southcentralus.cloudapp.azure.com/
```

### Launch edge runtime and 

Make sure you’re using a device connection string and not IoT Hub connection string if you get the error, “Connection string does not have a DeviceId element. Please supply a *device* connection string and not an Azure IoT Hub connection string.”

```
launch-edge-runtime -c "<IoT Hub device connection string>"
```

Use the edge cli to log into the IoT hub to which your edge device is registered. Note that you need the IoT hub’s owner connection string. You can find this in the Azure Portal by going to your IoT hub -> Shared Access Policies -> iothubowner

```
edge-explorer login "<IoT Hub connection string for iothubowner policy*>"
```

### Build your module

Get back to the directory we just created, you could specify the architecture folder name as you want, build the project:

```
dotnet build
dotnet publish -f netcoreapp2 -o .\Docker\Windows-x64\publish
```

### Create and run local docker registry

```
docker run -d -p 5000:5000 --name registry registry:2
```

### Build docker image for your module

```
docker build --build-arg EXE_DIR=./publish -t localhost:5000/<lower_case_module_name>:latest <docker_file_directory>
```

### Push the image to local registry

```
docker push localhost:5000/<lower_case_module_name>
```

### Deploy the module

Deployment is accomplished using the edge cli tool. This tool uses Azure IoT Hub to send deployment information to the edge device.

Update the deployment.json with lower case of your module name localhost:5000/<lower_case_module_name> before you run the following command:

```
edge-explorer edge deployment create -m <path to deployment file> -d <edge device ID>
```

Now we have the sample module deployed and running, you could monitor it with command 

```
edge-explorer monitor events <deviceID> --login <iothub connection string not device connection string>
```

There will be regular and continuing temperature message show in the console. If not, go back check if each step accomplished correctly.