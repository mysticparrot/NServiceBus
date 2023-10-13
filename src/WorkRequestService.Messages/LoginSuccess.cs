namespace WorkRequestService.Messages
{
    using NServiceBus;

    public class LoginSuccess : IEvent
    {
        public Guid? UserId { get; set; }
        public string? UserName { get; set; }
    }
}