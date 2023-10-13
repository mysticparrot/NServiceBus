namespace WorkRequestService
{
    using NServiceBus;
    using NServiceBus.Logging;
    using WorkRequestService.Messages;

    public class ComplaintSavedHandler :
   IHandleMessages<ComplaintSaved>
    {
        static ILog log = LogManager.GetLogger<ComplaintSavedHandler>();

        public Task Handle(ComplaintSaved message, IMessageHandlerContext context)
        {
            log.Info($"Saved complaint from UserName = {message.UserId}");
            return Task.CompletedTask;
        }
    }
}