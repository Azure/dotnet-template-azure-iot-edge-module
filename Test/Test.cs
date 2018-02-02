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
        private static string RelativeTemplatePath = @"../../../../content/dotnet-template-azure-iot-edge-module/";
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
        private const string CSharp = "C#";
        private const string FSharp = "F#";
        private const string ArchLinux64 = "linux64";
        private const string ArchWindowsNano = "windowsNano";

        public Test(DotNetFixture fixture)
        {
            this.fixture = fixture;
        }

        public static Dictionary<string, List<string>> FlagFilesMapping = new Dictionary<string, List<string>>{
            {
                ArchLinux64, new List<string> {
                    "Dockerfile",
                    "Dockerfile.amd64.debug"
                }
            },
            {
                ArchWindowsNano, new List<string> {
                    "Dockerfile"
                }
            }
        };

        private static string BeforeEach(string lang, string repository, bool linux64 = true, bool windowsNano = true, bool skipRestore = false)
        {
            var scaffoldName = Path.GetRandomFileName().Replace(".", "").ToString();
            var command = "new aziotedgemodule -n " + scaffoldName + " -lang " + lang + " -lx " + linux64 + " -wn " + windowsNano + " -s " + skipRestore + " -r " + repository;
            Process.Start("dotnet", command).WaitForExit();
            return scaffoldName;
        }

        [Theory]
        [InlineData(CSharp, true, true, true)]
        [InlineData(CSharp, true, true, false)]
        [InlineData(CSharp, true, false, true)]
        [InlineData(CSharp, true, false, false)]
        [InlineData(CSharp, false, true, true)]
        [InlineData(CSharp, false, true, false)]
        [InlineData(CSharp, false, false, true)]
        [InlineData(CSharp, false, false, false)]
        [InlineData(FSharp, true, true, true)]
        [InlineData(FSharp, true, true, false)]
        [InlineData(FSharp, true, false, true)]
        [InlineData(FSharp, true, false, false)]
        [InlineData(FSharp, false, true, true)]
        [InlineData(FSharp, false, true, false)]
        [InlineData(FSharp, false, false, true)]
        [InlineData(FSharp, false, false, false)]
        public void TestArchitecture(string lang, bool linux64, bool windowsNano, bool skipRestore)
        {
            var repository = "test.azurecr.io/test";
            var scaffoldName = BeforeEach(lang, repository, linux64, windowsNano, skipRestore);
            var filesToCheck = new List<string> { ".gitignore", "module.json" };

            if (skipRestore)
            {
                Assert.True(!Directory.Exists(Path.Combine(scaffoldName, "obj")));
            }
            else
            {
                Assert.True(Directory.Exists(Path.Combine(scaffoldName, "obj")));
            }

            if (lang == CSharp)
            {
                filesToCheck.AddRange(new List<string> { "Program.cs", scaffoldName + ".csproj" });
            }
            if (lang == FSharp)
            {
                filesToCheck.AddRange(new List<string> { "Program.fs", scaffoldName + ".fsproj" });
            }

            if (linux64)
            {
                filesToCheck.AddRange(FlagFilesMapping[ArchLinux64]);

            }
            if (windowsNano)
            {
                filesToCheck.AddRange(FlagFilesMapping[ArchWindowsNano]);
            }

            foreach (var file in filesToCheck)
            {
                Assert.True(File.Exists(Path.Combine(scaffoldName, file)));
            }

            string text = File.ReadAllText(Path.Combine(scaffoldName, "module.json"));
            Assert.Contains(repository, text);

            Directory.Delete(scaffoldName, true);
        }
    }
}
