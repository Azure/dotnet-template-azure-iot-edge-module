namespace SampleModule

open System
open System.IO
open System.Runtime.InteropServices
open System.Runtime.Loader
open System.Security.Cryptography.X509Certificates
open System.Text
open System.Threading
open System.Threading.Tasks
open Microsoft.Azure.Devices.Client
open Microsoft.Azure.Devices.Client.Transport.Mqtt

module SampleModule = 

    let counter = ref 0 

    let awaitTask (t: Task) = t.ContinueWith (ignore) |> Async.AwaitTask

    let PipeMessage (message:Message) (userContext:obj) =
        let counterValue = Interlocked.Increment(counter)

        let moduleClient = userContext :?> ModuleClient
        if (isNull(moduleClient)) then
            raise (InvalidOperationException("UserContext doesn't contain " + "expected values"))

        let messageBytes = message.GetBytes()
        let messageString = Encoding.UTF8.GetString(messageBytes)
        printfn "Received message: %i, Body: [%s]" counterValue messageString

        if (not (String.IsNullOrEmpty(messageString))) then
            let pipeMessage = new Message(messageBytes)
            
            message.Properties 
            |> Seq.iter (fun prop -> pipeMessage.Properties.Add(prop.Key, prop.Value))
            
            moduleClient.SendEventAsync("output1", pipeMessage) 
            |> Async.AwaitTask 
            |> Async.Start

            Console.WriteLine("Received message sent");
        
        Task.FromResult (MessageResponse.Completed)

    let Init (connectionString:string) (bypassCertVerification:bool) =
        printfn "Connection String %s" connectionString

        let mqttSetting = MqttTransportSettings(TransportType.Mqtt_Tcp_Only)
        // During dev you might want to bypass the cert verification. 
        // It is highly recommended to verify certs systematically in production
        if bypassCertVerification then
            mqttSetting.RemoteCertificateValidationCallback <- (fun _ _ _ _ -> true) 
        let transportSettings = mqttSetting :> ITransportSettings
        let settings = [|transportSettings|] 

        // Open a connection to the Edge runtime
        let ioTHubModuleClient = 
            ModuleClient.CreateFromConnectionString(connectionString, settings)

        ioTHubModuleClient.OpenAsync() 
        |> Async.AwaitTask 
        |> Async.Start
        
        printfn "IoT Hub module client initialized."

        // Register callback to be called when a message is received by the module
        let pipeMessageHandler = new MessageHandler(PipeMessage)

        ioTHubModuleClient.SetInputMessageHandlerAsync("input1", pipeMessageHandler, ioTHubModuleClient)
        |> Async.AwaitTask 
        |> Async.Start
        

    let InstallCert () =
        let certPath = Environment.GetEnvironmentVariable("EdgeModuleCACertificateFile");
        
        if (String.IsNullOrWhiteSpace(certPath)) then
            // We cannot proceed further without a proper cert file
            printfn "Missing path to certificate collection file: %s" certPath
            raise (InvalidOperationException("Missing path to certificate file."))
        elif (not (File.Exists(certPath))) then
            // We cannot proceed further without a proper cert file
            printfn "Missing path to certificate collection file: %s" certPath
            raise (InvalidOperationException("Missing certificate file."))
        else
            let store = new X509Store(StoreName.Root, StoreLocation.CurrentUser)
            store.Open(OpenFlags.ReadWrite)
            store.Add(new X509Certificate2(X509Certificate2.CreateFromCertFile(certPath)))
            Console.WriteLine("Added Cert: " + certPath)
            store.Close()
        

    let WhenCancelled (cancellationToken:CancellationToken) : Task<bool> =
            let tcs = new TaskCompletionSource<bool>()
            let setTcsResult = 
                Action<obj>(fun s -> (s :?> TaskCompletionSource<bool>).SetResult(true))
            cancellationToken.Register(setTcsResult, tcs) |> ignore
            tcs.Task
        

    [<EntryPoint>]
    let main _ =
        let connectionString = Environment.GetEnvironmentVariable("EdgeHubConnectionString")

        // Cert verification is not yet fully functional when using Windows OS for the container
        let bypassCertVerification = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
        if (not bypassCertVerification) then InstallCert ()
        Init connectionString bypassCertVerification

        // Wait until the app unloads or is cancelled
        let cts = new CancellationTokenSource()
        AssemblyLoadContext.Default.add_Unloading(fun _ -> cts.Cancel())
        Console.CancelKeyPress.Add(fun _ -> cts.Cancel())
        WhenCancelled(cts.Token).Wait()
        0 // return an integer exit code
