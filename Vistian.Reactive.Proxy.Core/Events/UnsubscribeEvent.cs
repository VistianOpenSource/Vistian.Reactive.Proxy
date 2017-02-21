namespace Vistian.Reactive.Proxy.Events
{
    public class UnsubscribeEvent : Event, IUnsubscribeEvent
    {
        public long SubscriptionId { get; private set; }

        public UnsubscribeEvent(long subscriptionId)
            : base(EventType.Unsubscribe)
        {
            SubscriptionId = subscriptionId;
        }
    }
}
