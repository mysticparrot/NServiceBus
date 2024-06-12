﻿namespace NServiceBus.AcceptanceTests.TimeToBeReceived;

using System;
using System.Threading;
using System.Threading.Tasks;
using AcceptanceTesting;
using EndpointTemplates;
using Features;
using NUnit.Framework;

public class When_TimeToBeReceived_has_expired_convention : NServiceBusAcceptanceTest
{
    [Test]
    public async Task Message_should_not_be_received()
    {
        var start = DateTime.UtcNow;

        var context = await Scenario.Define<Context>()
            .WithEndpoint<Endpoint>()
            .Done(c => c.WasCalled || DateTime.UtcNow - start > TimeSpan.FromSeconds(15))
            .Run();

        Assert.IsFalse(context.WasCalled);
    }

    public class Context : ScenarioContext
    {
        public bool WasCalled { get; set; }
    }

    class DelayReceiverFromStartingTask : FeatureStartupTask
    {
        protected override async Task OnStart(IMessageSession session, CancellationToken cancellationToken = default)
        {
            await session.SendLocal(new MyMessage(), cancellationToken: cancellationToken);
            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
        }

        protected override Task OnStop(IMessageSession session, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }

    public class Endpoint : EndpointConfigurationBuilder
    {
        public Endpoint()
        {
            EndpointSetup<DefaultServer>(c =>
            {
                c.RegisterStartupTask(new DelayReceiverFromStartingTask());
                c.Conventions().DefiningTimeToBeReceivedAs(messageType =>
                {
                    if (messageType == typeof(MyMessage))
                    {
                        return TimeSpan.FromSeconds(2);
                    }
                    return TimeSpan.MaxValue;
                });
            });
        }

        public class MyMessageHandler : IHandleMessages<MyMessage>
        {
            public MyMessageHandler(Context context)
            {
                testContext = context;
            }

            public Task Handle(MyMessage message, IMessageHandlerContext context)
            {
                testContext.WasCalled = true;
                return Task.CompletedTask;
            }

            Context testContext;
        }
    }

    public class MyMessage : IMessage
    {
    }
}