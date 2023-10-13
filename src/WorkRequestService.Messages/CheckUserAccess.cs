﻿namespace WorkRequestService.Messages
{
    using NServiceBus;

    public class CheckUserAccess : ICommand
    {
        public Guid? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}