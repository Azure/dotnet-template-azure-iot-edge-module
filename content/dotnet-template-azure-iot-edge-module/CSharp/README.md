# IoT Edge Module Template for .NET 7

This project leverages the latest dotnet features to create docker images without using a `Dockerfile`. See more details in https://github.com/dotnet/sdk-container-builds

## Create the docker image

```
dotnet publish --os linux --arch x64 -c Release /t:PublishContainer
```

## Debug

The `Properties\launchSettings.TEMPLATE.json` shows how to add an environment variable to debug. Rename the file to remove `TEMPLATE` and update the module connection string.

## Publish to a container registry

The created image can be re-tagged to match your target container registry, or you build with the MSBuild property `ContainerRegistry` to produce the image for your registry
