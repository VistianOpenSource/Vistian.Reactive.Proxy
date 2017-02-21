using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vistian.Reactive.Proxy.EventHandlers.Snapshot.Local;
using Vistian.Reactive.Proxy.Events;

namespace Vistian.Reactive.Proxy.EventHandlers.Exceptions
{
    /// <summary>
    /// Error trap reactive extensions proxy handler.
    /// </summary>
    public class Handler: IEventHandler
    {
        private readonly State _state;

        public event EventHandler<OnErrorExceptionEventArgs> ErrorRaised;

        public Handler(State state)
        {
            _state = state;
        }

        public void Dispose()
        {
        }

        public void OnCreated(IOperatorCreatedEvent onCreatedEvent)
        {
        }

        public void OnCompleted(IOnCompletedEvent onCompletedEvent)
        {
        }

        public void OnError(IOnErrorEvent onErrorEvent)
        {
            // right then, we need to look in the state to possibly produce an event
            OnErrorRaised(new OnErrorExceptionEventArgs(onErrorEvent,_state));
        }

        protected virtual void OnErrorRaised(OnErrorExceptionEventArgs e)
        {
            ErrorRaised?.Invoke(this, e);
        }

        public void OnNext(IOnNextEvent onNextEvent)
        {
        }

        public void OnSubscribe(ISubscribeEvent subscribeEvent)
        {
        }

        public void OnUnsubscribe(IUnsubscribeEvent unsubscribeEvent)
        {
        }

        public void OnConnected(IConnectedEvent connectedEvent)
        {
        }

        public void OnDisconnected(IDisconnectedEvent disconnectedEvent)
        {
        }

        public void OnTag(ITagOperatorEvent tagEvent)
        {
        }
    }
}
