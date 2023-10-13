namespace WorkRequestService.Messages
{
    using NServiceBus;

    public class ComplaintSaved : IEvent
    {
        public Guid? UserId { get; set; }

    }
}