﻿namespace Sales.Messages
{
    using NServiceBus;

    public class PlaceOrder :
        ICommand
    {
        public string? OrderId { get; set; }
    }
}