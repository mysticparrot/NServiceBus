namespace LoginUI
{
    using NServiceBus;
    using NServiceBus.Logging;
    using WorkRequestService.Messages;

    public class LoginFailedHandler :
    IHandleMessages<LoginFailed>
    {
        static ILog log = LogManager.GetLogger<LoginFailedHandler>();

        public Task Handle(LoginFailed message, IMessageHandlerContext context)
        {
            log.Info($"LoginFailed for UserName = {message.UserName}");
            return Task.CompletedTask;
        }
    }
}