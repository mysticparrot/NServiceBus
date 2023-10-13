namespace WorkRequestEntryUI
{
    using NServiceBus;
    using NServiceBus.Logging;
    using WorkRequestService.Messages;

    public class AddComplaintsHandler :
    IHandleMessages<AddComplaint>
    {
        static ILog log = LogManager.GetLogger<AddComplaintsHandler>();

        public Task Handle(AddComplaint message, IMessageHandlerContext context)
        {
            log.Info($"AddComplaint event received for UserId = {message.UserId}");

            var complaintSaved = new ComplaintSaved()
            {
                UserId = message.UserId,
            };
            return context.Publish(complaintSaved);
        }
    }
}