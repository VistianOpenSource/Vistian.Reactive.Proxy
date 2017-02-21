using System;
using Vistian.Reactive.Proxy.Events;

namespace Vistian.Reactive.Proxy.Extensions
{
    public static class EventHandlerExtensions
    {
        private static long Publish<T>(Action<T> publishAction, T ev) where T: IEvent
        {
            publishAction(ev);
            return ev.EventId;
        }

        public static long OnConnected(this IEventHandler This, OperatorInfo operatorInfo)
        {
            return Publish(This.OnConnected, Event.Connect(operatorInfo));
        }

        public static long OnDisconnected(this IEventHandler This, long subscriptionId)
        {
            return Publish(This.OnDisconnected, Event.Disconnect(subscriptionId));
        }

        public static long OnSubscribe(this IEventHandler This, OperatorInfo child, OperatorInfo parent)
        {
            return Publish(This.OnSubscribe, Event.Subscribe(child, parent));
        }

        public static long OnUnsubscribe(this IEventHandler This, long subscriptionId)
        {
            return Publish(This.OnUnsubscribe, Event.Unsubscribe(subscriptionId));
        }
    }
}
