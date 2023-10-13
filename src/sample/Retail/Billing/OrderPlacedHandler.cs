namespace Billing
{
    using Billing.Messages;
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

            var orderBilled = new OrderBilled()
            {
                OrderId = message.OrderId,
            };
            return context.Publish(orderBilled);
        }
    }
}