## Prerequisites
* Latest [Visual Studio Code](https://code.visualstudio.com/)
* [.NET Core SDK](https://www.microsoft.com/net/download/windows/)

## Setup
1. Fork and clone the repository
2. Install the .NET Core SDK
3. Open the folder in VS Code
4. Install the template `dotnet new -i ./content/dotnet-template-azure-iot-edge-module`
5. Use the template to scaffolding `dotnet new aziotedgemodule -n YourModuleName`

## Test
Make sure you have run `dotnet test` before submitting Pull Requests.