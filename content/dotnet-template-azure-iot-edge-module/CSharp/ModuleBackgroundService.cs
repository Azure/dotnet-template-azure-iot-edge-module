using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Client.Transport.Mqtt;
using System.Text;

namespace SampleModule;

internal class ModuleBackgroundService : BackgroundService
{
    private int _counter;
    private readonly ILogger<ModuleBackgroundService> _logger;

    public ModuleBackgroundService(ILogger<ModuleBackgroundService> logger) => _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        MqttTransportSettings mqttSetting = new(TransportType.Mqtt_Tcp_Only);
        ITransportSettings[] settings = { mqttSetting };

        // Open a connection to the Edge runtime
        ModuleClient ioTHubModuleClient = await ModuleClient.CreateFromEnvironmentAsync(settings);
        await ioTHubModuleClient.OpenAsync(stoppingToken);
        _logger.LogInformation("IoT Hub module client initialized.");

        // Register callback to be called when a message is received by the module
        await ioTHubModuleClient.SetInputMessageHandlerAsync("input1", PipeMessage, ioTHubModuleClient, stoppingToken);
    }

    async Task<MessageResponse> PipeMessage(Message message, object userContext)
    {
        int counterValue = Interlocked.Increment(ref _counter);

        if (userContext is not ModuleClient moduleClient)
        {
            throw new InvalidOperationException("UserContext doesn't contain " + "expected values");
        }

        byte[] messageBytes = message.GetBytes();
        string messageString = Encoding.UTF8.GetString(messageBytes);
        _logger.LogInformation("Received message: {counterValue}, Body: [{messageString}]", counterValue, messageString);

        if (!string.IsNullOrEmpty(messageString))
        {
            using var pipeMessage = new Message(messageBytes);
            foreach (var prop in message.Properties)
            {
                pipeMessage.Properties.Add(prop.Key, prop.Value);
            }
            await moduleClient.SendEventAsync("output1", pipeMessage);

            _logger.LogInformation("Received message sent");
        }
        return MessageResponse.Completed;
    }
}
