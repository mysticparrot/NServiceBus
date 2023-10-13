namespace WorkRequestService
{
    using NServiceBus;

    class Program
    {
        static async Task Main()
        {

            Console.Title = "Work Request Service";

            var endpointConfiguration = new EndpointConfiguration("WorkRequestService");
            // Choose JSON to serialize and deserialize messages
            endpointConfiguration.UseSerialization<SystemJsonSerializer>();

            _ = endpointConfiguration.UseTransport<LearningTransport>();

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}