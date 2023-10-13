namespace LoginUI
{
    using NServiceBus;
    using NServiceBus.Logging;
    using WorkRequestService.Messages;

    class Program
    {
        static async Task Main()
        {
            Console.Title = "Login UI";

            var endpointConfiguration = new EndpointConfiguration("LoginUI");
            // Choose JSON to serialize and deserialize messages
            endpointConfiguration.UseSerialization<SystemJsonSerializer>();

            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(CheckUserAccess), "WorkRequestService");

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
                log.Info("Press 'L' to Log in, or 'Q' to quit.");
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key.ToString())
                {
                    case "L":
                        // Instantiate the command
                        var command = new CheckUserAccess
                        {
                            UserId = Guid.NewGuid(),
                            UserName = Faker.Name.First().ToLower(),
                            Password = Faker.Name.First().ToLower().GetHashCode().ToString(),
                        };

                        // Send the command to the local endpoint
                        log.Info($"Check User Access UserName = {command.UserName}");
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