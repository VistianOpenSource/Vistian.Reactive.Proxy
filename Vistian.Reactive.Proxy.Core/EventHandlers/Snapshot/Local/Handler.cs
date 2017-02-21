using Vistian.Reactive.Proxy.Events;

namespace Vistian.Reactive.Proxy.EventHandlers.Snapshot.Local
{
    public class Handler:IEventHandler
    {
        private readonly State _state;

        public Handler(State state)
        {
            _state = state;
        }

        public void Dispose()
        {          
        }

        public void OnCreated(IOperatorCreatedEvent onCreatedEvent)
        {
            _state.OnEvent(onCreatedEvent);
        }

        public void OnCompleted(IOnCompletedEvent onCompletedEvent)
        {
            _state.OnEvent(onCompletedEvent);
        }

        public void OnError(IOnErrorEvent onErrorEvent)
        {
            _state.OnEvent(onErrorEvent);
        }

        public void OnNext(IOnNextEvent onNextEvent)
        {
            _state.OnEvent(onNextEvent);
        }

        public void OnSubscribe(ISubscribeEvent subscribeEvent)
        {
            _state.OnEvent(subscribeEvent);
        }

        public void OnUnsubscribe(IUnsubscribeEvent unsubscribeEvent)
        {
            _state.OnEvent(unsubscribeEvent);
        }

        public void OnConnected(IConnectedEvent connectedEvent)
        {
            _state.OnEvent(connectedEvent);
        }

        public void OnDisconnected(IDisconnectedEvent disconnectedEvent)
        {
            _state.OnEvent(disconnectedEvent);
        }

        public void OnTag(ITagOperatorEvent tagEvent)
        {
            _state.OnEvent(tagEvent);
        }
    }
}