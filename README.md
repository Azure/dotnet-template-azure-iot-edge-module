# dotnet-template-azure-iot-edge-module
> dotnet template to do scaffolding tool for azure iot edge module development.

This ReadMe consists of two parts:
- Get Started to introcude how to install the dotnet template nuget package step by step
- Containerize the module

  The dotnet template sets up all necessary files for you to focus on module functionality programming.

  After the coding part completed, following the steps in this part to leverage docker to containerize your module so theat they can be deployed and monitored by the new features of Azure IoT Edge more straight forward.

## Get Started

Make sure you have [Nuget](https://www.nuget.org/) installed.

Run following command to add the template nuget source:

```
nuget sources add -name AzureIoTEdgeModuleGenerator -source https://www.myget.org/F/dotnet-template-azure-iot-edge-module/api/v2
```

Check the nuget source is added successfully and enabled with command **nuget sources**:

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

run dotnet command to install the template:
```
dotnet new -i <~/dotnet-template-azure-iot-edge-module/CSharp>
```

check out all arguments the template may take:
```
dotnet net aziotedgemodulegen --help
```

A full list as following:

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

Now create the module:

```
dotnet new aziotedgemodulegen -n <ProjectName> -mo <ModuleName> -m CM -wx true -lx true
```
We support multiple architectures, so users have to specify all the architectures corresponding arguments to true to enable it. windows-x64 is default true.

You could refer to above list for argument meaning.

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

Now navigate to the genarated module folder in the first place.

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
