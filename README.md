# dotnet-template-azure-iot-edge-module
> dotnet template to do scaffolding tool for azure iot edge module development.

## Get Started

Clone the repo to local: 
```
git clone https://github.com/SummerSun/dotnet-template-azure-iot-edge-module
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
dotnet new aziotedgemodulegen -n <ProjectName> -mo <ModuleName> -m CM -wx true -l6 true
```
We support multiple architectures, so users have to specify all the architectures corresponding arguments to true to enable it. windows-x64 is default true.

You could refer to above list for argument meaning.

## Next Steps
To be documented.