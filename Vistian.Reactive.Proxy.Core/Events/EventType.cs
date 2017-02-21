namespace Vistian.Reactive.Proxy.Events
{
    public enum EventType
    {
        OperatorCreated,
        OperatorCollected,
        Subscribe,
        Unsubscribe,

        OnNext,
        OnError,
        OnCompleted,

        TagOperator,

        Connected,
        Disconnected
    }
}
