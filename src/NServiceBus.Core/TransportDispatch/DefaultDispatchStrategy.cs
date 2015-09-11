﻿namespace NServiceBus.TransportDispatch
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using NServiceBus.DeliveryConstraints;
    using NServiceBus.Pipeline;
    using NServiceBus.Routing;
    using NServiceBus.Transports;

    class DefaultDispatchStrategy : DispatchStrategy
    {
        public override Task Dispatch(IDispatchMessages dispatcher, OutgoingMessage message, RoutingStrategy routingStrategy, IEnumerable<DeliveryConstraint> constraints, BehaviorContext currentContext, DispatchConsistency dispatchConsistency)
        {
            return dispatcher.Dispatch(message, new DispatchOptions(routingStrategy, currentContext, constraints, dispatchConsistency));
        }
    }
}