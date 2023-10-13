namespace DashboardUI
{
    using NServiceBus;
    using NServiceBus.Logging;
    using WorkRequestService.Messages;

    public class LoginSuccessHandler :
    IHandleMessages<LoginSuccess>
    {
        static ILog log = LogManager.GetLogger<LoginSuccessHandler>();

        public Task Handle(LoginSuccess message, IMessageHandlerContext context)
        {
            log.Info($"LoginSuccess for UserName = {message.UserName}");
            var addComplaint = new AddComplaint()
            {
                UserId = message.UserId,
            };
            return context.Publish(addComplaint);
        }
    }
}