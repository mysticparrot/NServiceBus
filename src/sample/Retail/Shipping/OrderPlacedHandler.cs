namespace Shipping
{
    using NServiceBus;
    using NServiceBus.Logging;
    using Sales.Messages;

    public class OrderPlacedHandler :
    IHandleMessages<OrderPlaced>
    {
        static ILog log = LogManager.GetLogger<OrderPlacedHandler>();

        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            log.Info($"Received OrderPlaced Event, OrderId = {message.OrderId} - Charging credit card...");
            return Task.CompletedTask;
        }
    }
}