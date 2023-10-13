namespace DashboardUI
{
    using NServiceBus;
    using WorkRequestService.Messages;

    class Program
    {
        static async Task Main()
        {

            Console.Title = "Dashboard UI";

            var endpointConfiguration = new EndpointConfiguration("DashboardUI");
            // Choose JSON to serialize and deserialize messages
            endpointConfiguration.UseSerialization<SystemJsonSerializer>();

            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(AddComplaint), "WorkRequestingUI");

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            // Replace with:
            //await RunLoop(endpointInstance, CancellationToken.None)
            //    .ConfigureAwait(false);

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }

        //static ILog log = LogManager.GetLogger<Program>();

        //static async Task RunLoop(IEndpointInstance endpointInstance, CancellationToken cancellationToken)
        //{
        //    while (true)
        //    {
        //        log.Info("Press 'L' to Log in, or 'Q' to quit.");
        //        var key = Console.ReadKey();
        //        Console.WriteLine();

        //        switch (key.Key.ToString())
        //        {
        //            case "L":
        //                // Instantiate the command
        //                var message = new AddComplaint
        //                {
        //                    UserId = Guid.NewGuid()
        //                };

        //                // Send the command to the local endpoint
        //                log.Info($"Add Complaint from UserId = {message.UserId}");
        //                await endpointInstance.Send(message, cancellationToken)
        //                    .ConfigureAwait(false);

        //                break;

        //            case "Q":
        //                return;

        //            default:
        //                log.Info("Unknown input. Please try again.");
        //                break;
        //        }
        //    }
        //}

    }
}