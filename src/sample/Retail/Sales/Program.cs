namespace Sales
{
    using NServiceBus;

    partial class Program
    {
        static async Task Main()
        {
            Console.Title = "Sales";

            var endpointConfiguration = new EndpointConfiguration("Sales");
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