using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Client.Transport.Mqtt;
using System.Text;

namespace SampleModule;

internal class ModuleBackgroundService : BackgroundService
{
    private int _counter;
    private ModuleClient? _moduleClient;
    private CancellationToken _cancellationToken;
    private readonly ILogger<ModuleBackgroundService> _logger;

    public ModuleBackgroundService(ILogger<ModuleBackgroundService> logger) => _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _cancellationToken = cancellationToken;
        MqttTransportSettings mqttSetting = new(TransportType.Mqtt_Tcp_Only);
        ITransportSettings[] settings = { mqttSetting };

        // Open a connection to the Edge runtime
        _moduleClient = await ModuleClient.CreateFromEnvironmentAsync(settings);

        // Reconnect is not implented because we'll let docker restart the process when the connection is lost
        _moduleClient.SetConnectionStatusChangesHandler((status, reason) => 
            _logger.LogWarning("Connection changed: Status: {status} Reason: {reason}", status, reason));

        await _moduleClient.OpenAsync(cancellationToken);

        _logger.LogInformation("IoT Hub module client initialized.");

        // Register callback to be called when a message is received by the module
        await _moduleClient.SetInputMessageHandlerAsync("input1", ProcessMessageAsync, null, cancellationToken);
    }

    async Task<MessageResponse> ProcessMessageAsync(Message message, object userContext)
    {
        int counterValue = Interlocked.Increment(ref _counter);

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
            await _moduleClient!.SendEventAsync("output1", pipeMessage, _cancellationToken);

            _logger.LogInformation("Received message sent");
        }
        return MessageResponse.Completed;
    }
}
