# Changelog

[![Nuget](https://img.shields.io/nuget/v/Microsoft.Azure.IoT.Edge.Module.svg)](https://www.nuget.org/packages/Microsoft.Azure.IoT.Edge.Module/)

### 3.2.0 (2022-11-29)
* [Updated] Update template to target dotnet6

### 3.1.1 (2021-09-01)
* [Updated] Updated the nuget template json to match the schema

### 3.1.0 (2020-04-07)
* [Updated] Add arm64v8 and arm64v8.debug dockerfiles
* [Updated] Upgrade .Net Core SDK to 3.1

### 2.7.0 (2019-12-20)
* [Updated] Pull base image from Microsoft Container Registry

### 2.6.0 (2019-11-01)
* [Added] Add .dockerignore to ignore bin and obj folder

### 2.5.0 (2019-03-05)
* [Added] Add arm32v7.debug dockerfile

### 2.4.0 (2018-12-18)
* [Updated] Update Windows base image for Windows 10 Version 1809
* [Updated] Switch back to MQTT as default transport

### 2.3.0 (2018-10-29)
* [Added] Add contextPath in module.json
* [Added] Add AzureIoTEdgeModule project capability for csproj
* [Updated] Use 1.* for Microsoft.Azure.Devices.Client

### 2.2.0 (2018-09-11)
* [Updated] Update Microsoft.Azure.Devices.Client dependency to 1.18.0 for CSharp Template
* [Updated] Update target framework to dotnet 2.1

### 2.1.0 (2018-07-26)
* [Updated] Update the sample code to use amqp instead of mqtt

### 2.0.0 (2018-06-27)
* [Updated] Update Microsoft.Azure.Devices.Client dependency to 1.17.0 for CSharp Template
* [Updated] Update the sample code to use module client instead of device client

### 1.4.0 (2018-04-27)
* [Updated] Use unprivileged user in amd64 and amd32v7 Dockerfiles

### 1.3.1 (2018-03-29)
* [Updated] Cache vsdbg in first layer in Docker file to speed up docker image rebuild time.

### 1.3.0 (2018-03-27)
* [Updated] Update folder structure to put docker files under project root
* [Added] Add module.json file into project root

### 1.2.0 (2017-12-22)
* [Fixed] Users get no hint with building Windows image against Docker Linux container
* [Added] Add support to scaffold Azure IoT Edge Custom Module in FSharp

### 1.1.0 (2017-12-06)
* [Fixed] Docker image hint in deployment.json is not clear enough

### 1.0.1 (2017-11-15)
* [Added] Add support to scaffold Azure IoT Edge Custom Module in CSharp
* [Added] Add support to scaffold only Azure IoT Edge deployment manifest
* [Added] Add support to scaffold targeting only linux-x64 or windows-nano architecture
* [Added] Add ready-to-use Azure IoT Edge Custom Module template
