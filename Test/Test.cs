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

        private static string BeforeEach(string lang, string repository, bool skipRestore = false)
        {
            var scaffoldName = Path.GetRandomFileName().Replace(".", "").ToString();
            var command = "new aziotedgemodule -n " + scaffoldName + " -lang " + lang  + " -s " + skipRestore + " -r " + repository;
            Process.Start("dotnet", command).WaitForExit();
            return scaffoldName;
        }

        [Theory]
        [InlineData(CSharp, true)]
        [InlineData(CSharp, false)]
        [InlineData(FSharp, true)]
        [InlineData(FSharp, false)]
        public void TestArchitecture(string lang, bool skipRestore)
        {
            var repository = "test.azurecr.io/test";
            var scaffoldName = BeforeEach(lang, repository, skipRestore);
            var filesToCheck = new List<string> { ".gitignore", "module.json", "Dockerfile", "Dockerfile.amd64.debug", "Dockerfile.arm32v7" };

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
