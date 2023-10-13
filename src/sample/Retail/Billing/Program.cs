namespace Billing
{
    using NServiceBus;

    class Program
    {
        static async Task Main()
        {

            Console.Title = "Billing";

            var endpointConfiguration = new EndpointConfiguration("Billing");
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