namespace WorkRequestService.Messages
{
    using NServiceBus;

    public class LoginFailed : IEvent
    {
        public Guid? UserId { get; set; }
        public string? UserName { get; set; }
    }
}