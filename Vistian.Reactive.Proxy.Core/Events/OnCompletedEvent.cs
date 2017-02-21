namespace Vistian.Reactive.Proxy.Events
{
    public class OnCompletedEvent : Event, IOnCompletedEvent
    {
        public long OperatorId { get; private set; }

        public OnCompletedEvent(OperatorInfo operatorInfo)
            : base(EventType.OnCompleted)
        {
            OperatorId = operatorInfo.Id;
        }
    }
}
