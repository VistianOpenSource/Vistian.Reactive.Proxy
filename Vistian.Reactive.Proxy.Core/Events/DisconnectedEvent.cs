namespace Vistian.Reactive.Proxy.Events
{
    public class DisconnectedEvent : Event, IDisconnectedEvent
    {
        public long ConnectionId { get; set; }

        public DisconnectedEvent(long connectionId)
            : base(EventType.Disconnected)
        {
            ConnectionId = connectionId;
        }
    }
}
