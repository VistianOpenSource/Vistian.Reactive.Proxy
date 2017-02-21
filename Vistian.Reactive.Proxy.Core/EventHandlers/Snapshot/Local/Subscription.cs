using System;
using Vistian.Reactive.Proxy.Events;

namespace Vistian.Reactive.Proxy.EventHandlers.Snapshot.Local
{
    public class Subscription
    {
        public long SubscriptionId { get; private set; }

        public ObservableState Parent { get; private set; }

        public ObservableState Child { get; private set; }

        public bool IsActive { get; set; }

        public TimeSpan Created { get; set; }

        public Subscription(ISubscribeEvent subscribeEvent, ObservableState child, ObservableState parent)
        {
            SubscriptionId = subscribeEvent.EventId;
            Parent = parent;
            Child = child;
            IsActive = true;
            Created = TimeSpan.FromMilliseconds(subscribeEvent.EventTime);
        }

    }
}
