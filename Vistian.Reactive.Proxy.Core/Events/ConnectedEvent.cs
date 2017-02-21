namespace Vistian.Reactive.Proxy.Events
{
    public class ConnectedEvent : Event, IConnectedEvent
    {
        public long OperatorId { get; set; }

        public ConnectedEvent(OperatorInfo operatorInfo)
            : base(EventType.Connected)
        {
            OperatorId = operatorInfo.Id;
        }
    }
}
