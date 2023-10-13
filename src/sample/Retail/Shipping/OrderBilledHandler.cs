namespace Shipping
{
    using Billing.Messages;
    using NServiceBus;
    using NServiceBus.Logging;

    public class OrderBilledHandler :
    IHandleMessages<OrderBilled>
    {
        static ILog log = LogManager.GetLogger<OrderBilledHandler>();

        public Task Handle(OrderBilled message, IMessageHandlerContext context)
        {
            log.Info($"Received OrderBilled Event, OrderId = {message.OrderId} - Charging credit card...");
            return Task.CompletedTask;
        }
    }
}