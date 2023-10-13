namespace Sales
{
    using NServiceBus;
    using NServiceBus.Logging;
    using Sales.Messages;

    partial class Program
    {
        public class PlaceOrderHandler :
    IHandleMessages<PlaceOrder>
        {
            static ILog log = LogManager.GetLogger<PlaceOrderHandler>();

            public Task Handle(PlaceOrder message, IMessageHandlerContext context)
            {
                log.Info($"Received PlaceOrder, OrderId = {message.OrderId}");
                var random = new Random();
                ;
                if (random.Next(0, 5) == 0)
                {
                    throw new Exception("BOOM");
                }
                var orderPlaced = new OrderPlaced
                {
                    OrderId = message.OrderId
                };
                return context.Publish(orderPlaced);
            }
        }
    }
}