using System;
using System.IO;
using System.Diagnostics;

namespace Microsoft.Azure.IoT.Edge.Module.Template
{
    class Program
    {
        static string TestModuleName = "TestModuleName";
        
        static void Main(string[] args)
        {
            Console.WriteLine("Installing latest dotnet template...");
            var proc = Process.Start("dotnet.exe", "new -i ../../content/dotnet-template-azure-iot-edge-module/CSharp/");
            proc.WaitForExit();
            Console.WriteLine("dotnet template installed");

            Console.WriteLine("Testing all files scenario...");
            if(!DefaultAllFiles())
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("All files scenario failed");
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("All files scenario pass");
            }
            Console.ResetColor();

            Console.WriteLine("Testing only deployment file scenario...");
            if(!DeploymentFile())
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Only deployment file scenario failed");
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("Only deployment file scenario pass!");
            }
            Console.ResetColor();

            Console.WriteLine("Testing only linux-x64 docker file scenario...");
            if(!OnlyLinuxDockerFile())
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Only linux-x64 Dockerfile scenario failed");
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("only linux-x64 Dockerfile scenario pass");
            }
            Console.ResetColor();
            
            Console.WriteLine("Testing only windows nano Dockerfile Scenario...");
            if(!OnlyWindowsDockerFile())
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Only windows-nano Dockerfile scenario failed");
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("Only windows-nano Dockerfile scenario pass");
            }
            Console.ResetColor();
        }

        static bool DefaultAllFiles() 
        {
            var proc = Process.Start("dotnet.exe", "new aziotedgemodule -n " + TestModuleName);
            proc.WaitForExit();
            var result = true;

            if(!File.Exists(TestModuleName + "/Program.cs"))
            {
                Console.WriteLine("program.cs not scaffolded");
                result = false;
            }
            if(!File.Exists(TestModuleName + "/" + TestModuleName + ".csproj"))
            {
                Console.WriteLine(".csproj not scaffolded");
                result = false;
            }
            if(!File.Exists(TestModuleName + "/deployment.json"))
            {
                Console.WriteLine("deployment.json not scaffolded");
                result = false;
            }
            if(!File.Exists(TestModuleName + "/Docker/linux-x64/Dockerfile"))
            {
                Console.WriteLine("linux-x64 Dockerfile not scaffolded");
                result = false;
            }
            if(!File.Exists(TestModuleName + "/Docker/windows-nano/Dockerfile"))
            {
                Console.WriteLine("windows-nano Dockerfile not scaffolded");
                result = false;
            }
            Directory.Delete(TestModuleName, true);
            return result;
        }

        static bool DeploymentFile() 
        {
            var proc = Process.Start("dotnet.exe", "new aziotedgemodule -t deploy -n " + TestModuleName);
            proc.WaitForExit();
            var result = true;

            if(File.Exists(TestModuleName + "/Program.cs"))
            {
                Console.WriteLine("program.cs should not be scaffolded");
                result = false;;
            }
            if(File.Exists(TestModuleName + "/" + TestModuleName + ".csproj"))
            {
                Console.WriteLine(".csproj should not be scaffolded");
                result = false;;
            }
            if(!File.Exists(TestModuleName + "/deployment.json"))
            {
                Console.WriteLine("deployment.json not scaffolded");
                result = false;;
            }
            if(File.Exists(TestModuleName + "/Docker/linux-x64/Dockerfile"))
            {
                Console.WriteLine("linux-x64 Dockerfile should not be scaffolded");
                result = false;;
            }
            if(File.Exists(TestModuleName + "/Docker/windows-nano/Dockerfile"))
            {
                Console.WriteLine("windows-nano Dockerfile should not be scaffolded");
                result = false;;
            }
            Directory.Delete(TestModuleName, true);
            return result;
        }
        static bool OnlyLinuxDockerFile() 
        {
            var proc = Process.Start("dotnet.exe", "new aziotedgemodule -wn false -n " + TestModuleName);
            proc.WaitForExit();
            var result = true;

            if(!File.Exists(TestModuleName + "/Program.cs"))
            {
                Console.WriteLine("program.cs not scaffolded");
                result = false;;
            }
            if(!File.Exists(TestModuleName + "/" + TestModuleName + ".csproj"))
            {
                Console.WriteLine(".csproj not scaffolded");
                result = false;;
            }
            if(!File.Exists(TestModuleName + "/deployment.json"))
            {
                Console.WriteLine("deployment.json not scaffolded");
                result = false;;
            }
            if(!File.Exists(TestModuleName + "/Docker/linux-x64/Dockerfile"))
            {
                Console.WriteLine("linux-x64 Dockerfile not scaffolded");
                result = false;;
            }
            if(File.Exists(TestModuleName + "/Docker/windows-nano/Dockerfile"))
            {
                Console.WriteLine("windows-nano Dockerfile should not be scaffolded");
                result = false;;
            }
            Directory.Delete(TestModuleName, true);
            return result;
        }
        static bool OnlyWindowsDockerFile() 
        {
            var proc = Process.Start("dotnet.exe", "new aziotedgemodule -lx false -n " + TestModuleName);
            proc.WaitForExit();
            var result = true;

            if(!File.Exists(TestModuleName + "/Program.cs"))
            {
                Console.WriteLine("program.cs not scaffolded");
                result = false;;
            }
            if(!File.Exists(TestModuleName + "/" + TestModuleName + ".csproj"))
            {
                Console.WriteLine(".csproj not scaffolded");
                result = false;;
            }
            if(!File.Exists(TestModuleName + "/deployment.json"))
            {
                Console.WriteLine("deployment.json not scaffolded");
                result = false;;
            }
            if(File.Exists(TestModuleName + "/Docker/linux-x64/Dockerfile"))
            {
                Console.WriteLine("linux-x64 Dockerfile should not be scaffolded");
                result = false;;
            }
            if(!File.Exists(TestModuleName + "/Docker/windows-nano/Dockerfile"))
            {
                Console.WriteLine("windows-nano Dockerfile not scaffolded");
                result = false;;
            }
            Directory.Delete(TestModuleName, true);
            return result;
        }
    }
}
