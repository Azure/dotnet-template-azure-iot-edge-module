using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xunit;

namespace Test
{
    public class DotNetFixture : IDisposable
    {
        private static string RelativeTemplatePath = @"../../../../content/dotnet-template-azure-iot-edge-module/CSharp/";
        public DotNetFixture()
        {
            Process.Start("dotnet", "new -i " + RelativeTemplatePath).WaitForExit();
        }

        public void Dispose()
        {
            Console.WriteLine("DotNetFixture: Disposing DotnetFixture");

            // uninstall does not work now according to dotnet issue
            // issue link: https://github.com/dotnet/templating/issues/1226
            Process.Start("dotnet", "new -u " + RelativeTemplatePath).WaitForExit();
        }
    }

    public class Test : IClassFixture<DotNetFixture>
    {
        private DotNetFixture fixture;
        private const string TargetAll = "all";
        private const string TargetDeploy = "deploy";
        private const string ArchLinux64 = "linux64";
        private const string ArchWindowsNano = "windowsNano";

        public Test(DotNetFixture fixture)
        {
            this.fixture = fixture;
        }

        public static Dictionary<string, List<string>> FlagFilesMapping = new Dictionary<string, List<string>>{
            {
                TargetAll, new List<string>()
            },
            {
                TargetDeploy, new List<string> {
                    "deployment.json"
                }
            },
            {
                ArchLinux64, new List<string> {
                    "Docker/linux-x64/Dockerfile",
                    "Docker/linux-x64/Dockerfile.debug"
                }
            },
            {
                ArchWindowsNano, new List<string> {
                    "Docker/windows-nano/Dockerfile"
                }
            }
        };

        private static string BeforeEach(string target = TargetAll, bool linux64 = true, bool windowsNano = true, bool skipRestore = false)
        {
            var scaffoldName = Path.GetRandomFileName().Replace(".", "").ToString();
            FlagFilesMapping[TargetAll] = new List<string> {scaffoldName + ".csproj", "Program.cs", ".gitignore"};
            var command = "new aziotedgemodule -n " + scaffoldName + " -t " + target + " -lx " + linux64 + " -wn " + windowsNano + " -s " + skipRestore;
            Process.Start("dotnet", command).WaitForExit();
            return scaffoldName;
        }

        [Theory]
        [InlineData(TargetAll, true, true, true)]
        [InlineData(TargetAll, true, true, false)]
        [InlineData(TargetAll, true, false, true)]
        [InlineData(TargetAll, true, false, false)]
        [InlineData(TargetAll, false, true, true)]
        [InlineData(TargetAll, false, true, false)]
        [InlineData(TargetAll, false, false, true)]
        [InlineData(TargetAll, false, false, false)]
        [InlineData(TargetDeploy, true, true, true)]
        [InlineData(TargetDeploy, true, true, false)]
        [InlineData(TargetDeploy, true, false, true)]
        [InlineData(TargetDeploy, true, false, false)]
        [InlineData(TargetDeploy, false, true, true)]
        [InlineData(TargetDeploy, false, true, false)]
        [InlineData(TargetDeploy, false, false, true)]
        [InlineData(TargetDeploy, false, false, false)]
        public void TestArchitecture(string target, bool linux64, bool windowsNano, bool skipRestore)
        {
            var scaffoldName = BeforeEach(target, linux64, windowsNano, skipRestore);            
            var filesToCheck = new List<string>();
            if(target == TargetDeploy)
            {
                filesToCheck = FlagFilesMapping[TargetDeploy];
            }
            else if (target == TargetAll)
            {
                if(skipRestore)
                {
                    Assert.True(!Directory.Exists(Path.Combine(scaffoldName, "obj")));
                }
                else
                {
                    Assert.True(Directory.Exists(Path.Combine(scaffoldName, "obj")));
                }

                filesToCheck = FlagFilesMapping[TargetDeploy].Union(FlagFilesMapping[TargetAll]).ToList();
                
                if (linux64)
                {
                    filesToCheck.AddRange(FlagFilesMapping[ArchLinux64]);

                }
                if (windowsNano)
                {
                    filesToCheck.AddRange(FlagFilesMapping[ArchWindowsNano]);
                }
            }

            foreach (var file in filesToCheck)
            {
                Assert.True(File.Exists(Path.Combine(scaffoldName, file)));
            }
            Directory.Delete(scaffoldName, true);
        }

        [Fact]
        public void TestDeployUnnecessaryFiles()
        {
            var scaffoldName = BeforeEach(TargetDeploy);
            var filesExistsToCheck = FlagFilesMapping[TargetDeploy];
            var filesNonExistsToCheck = FlagFilesMapping[ArchLinux64].Union(FlagFilesMapping[ArchWindowsNano]).Union(FlagFilesMapping[TargetAll]);

            foreach (var file in filesExistsToCheck)
            {
                Assert.True(File.Exists(Path.Combine(scaffoldName, file)));
            }
            foreach (var file in filesNonExistsToCheck)
            {
                Assert.True(!File.Exists(Path.Combine(scaffoldName, file)));
            }
            Assert.True(!Directory.Exists(Path.Combine(scaffoldName, "obj")));
            Directory.Delete(scaffoldName, true);
        }
    }
}
