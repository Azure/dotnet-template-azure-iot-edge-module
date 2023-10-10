using SampleModule;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>services.AddSingleton<ModuleBackgroundService>())
    .Build();

host.Run();