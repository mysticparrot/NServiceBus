namespace WorkRequestEntryUI
{
    using NServiceBus;
    using WorkRequestService.Messages;

    class Program
    {
        static async Task Main()
        {

            Console.Title = "Work Requesting UI";

            var endpointConfiguration = new EndpointConfiguration("WorkRequestingUI");
            // Choose JSON to serialize and deserialize messages
            endpointConfiguration.UseSerialization<SystemJsonSerializer>();

            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(ComplaintSaved), "WorkRequestService");

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}