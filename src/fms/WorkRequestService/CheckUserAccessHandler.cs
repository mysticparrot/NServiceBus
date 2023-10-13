namespace WorkRequestService
{
    using NServiceBus;
    using NServiceBus.Logging;
    using WorkRequestService.Messages;

    public class CheckUserAccessHandler :
    IHandleMessages<CheckUserAccess>
    {
        static ILog log = LogManager.GetLogger<CheckUserAccessHandler>();

        public Task Handle(CheckUserAccess message, IMessageHandlerContext context)
        {
            log.Info($"Received Check User Access for UserName = {message.UserName}");
            var random = new Random();
            if (random.Next(0, 5) == 0)
            {
                var failed = new LoginFailed
                {
                    UserId = message.UserId,
                    UserName = message.UserName
                };

                return context.Publish(failed);
            };

            var success = new LoginSuccess
            {
                UserId = message.UserId,
                UserName = message.UserName
            };

            return context.Publish(success);
        }
    }
}