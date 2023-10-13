namespace WorkRequestService.Messages
{
    using NServiceBus;

    public class AddComplaint : IEvent
    {
        public Guid? UserId { get; set; }
    }
}