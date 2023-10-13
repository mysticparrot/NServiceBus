namespace WorkRequestService.Messages
{
    using NServiceBus;

    public class SaveComplaint : ICommand
    {
        public Guid? UserId { get; set; }
    }
}