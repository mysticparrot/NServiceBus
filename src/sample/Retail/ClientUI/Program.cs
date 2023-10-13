namespace ClientUI
{
    using NServiceBus;
    using NServiceBus.Logging;
    using Sales.Messages;

    partial class Program
    {
        static async Task Main()
        {
            Console.Title = "ClientUI";

            var endpointConfiguration = new EndpointConfiguration("ClientUI");

            // Choose JSON to serialize and deserialize messages
            endpointConfiguration.UseSerialization<SystemJsonSerializer>();

            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(PlaceOrder), "Sales");

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);

            // Replace with:
            await RunLoop(endpointInstance, CancellationToken.None)
                .ConfigureAwait(false);

            await endpointInstance.Stop()
                .ConfigureAwait(false);

        }

        static ILog log = LogManager.GetLogger<Program>();

        static async Task RunLoop(IEndpointInstance endpointInstance, CancellationToken cancellationToken)
        {
            while (true)
            {
                log.Info("Press 'P' to place an order, or 'Q' to quit.");
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key.ToString())
                {
                    case "P":
                        // Instantiate the command
                        var command = new PlaceOrder
                        {
                            OrderId = Guid.NewGuid().ToString()
                        };

                        // Send the command to the local endpoint
                        log.Info($"Sending PlaceOrder command, OrderId = {command.OrderId}");
                        await endpointInstance.Send(command, cancellationToken)
                            .ConfigureAwait(false);

                        break;

                    case "Q":
                        return;

                    default:
                        log.Info("Unknown input. Please try again.");
                        break;
                }
            }
        }
    }
}