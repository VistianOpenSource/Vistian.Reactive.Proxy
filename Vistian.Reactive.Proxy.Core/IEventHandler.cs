using System;
using Vistian.Reactive.Proxy.Events;

namespace Vistian.Reactive.Proxy
{
    public interface IEventHandler: IDisposable
    {
        void OnCreated(IOperatorCreatedEvent onCreatedEvent);
        void OnCompleted(IOnCompletedEvent onCompletedEvent);
        void OnError(IOnErrorEvent onErrorEvent);
        void OnNext(IOnNextEvent onNextEvent);
        void OnSubscribe(ISubscribeEvent subscribeEvent);
        void OnUnsubscribe(IUnsubscribeEvent unsubscribeEvent);
        void OnConnected(IConnectedEvent connectedEvent);
        void OnDisconnected(IDisconnectedEvent disconnectedEvent);
        void OnTag(ITagOperatorEvent tagEvent);
    }
}
