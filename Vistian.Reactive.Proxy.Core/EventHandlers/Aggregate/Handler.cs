using System;
using System.Collections;
using System.Collections.Generic;
using Vistian.Reactive.Proxy.Events;

namespace Vistian.Reactive.Proxy.EventHandlers.Aggregate
{
    public class Handler : IEventHandler,IEnumerable<IEventHandler>
    {
        private readonly List<IEventHandler> _handlers = new List<IEventHandler>();

        public Handler()
        {
        }

        public Handler(IEnumerable<IEventHandler> handlers)
        {
            _handlers.AddRange(handlers);
        }


        private void ForAll(Action<IEventHandler> action)
        {
            foreach (var handler in _handlers)
            {
                action(handler);
            }
        }

        public void Dispose()
        {
            ForAll((h) => h.Dispose());
        }

        public void OnCreated(IOperatorCreatedEvent onCreatedEvent)
        {
            ForAll((h) => h.OnCreated(onCreatedEvent));
        }

        public void OnCompleted(IOnCompletedEvent onCompletedEvent)
        {
            ForAll((h) => h.OnCompleted(onCompletedEvent));

        }

        public void OnError(IOnErrorEvent onErrorEvent)
        {
            ForAll((h) => h.OnError(onErrorEvent));
        }

        public void OnNext(IOnNextEvent onNextEvent)
        {
            ForAll((h)=> h.OnNext(onNextEvent));
        }

        public void OnSubscribe(ISubscribeEvent subscribeEvent)
        {
            ForAll((h) => h.OnSubscribe(subscribeEvent));
        }

        public void OnUnsubscribe(IUnsubscribeEvent unsubscribeEvent)
        {
            ForAll((h) => h.OnUnsubscribe(unsubscribeEvent));
        }

        public void OnConnected(IConnectedEvent connectedEvent)
        {
            ForAll((h) => h.OnConnected(connectedEvent));
        }

        public void OnDisconnected(IDisconnectedEvent disconnectedEvent)
        {
            ForAll((h) => h.OnDisconnected(disconnectedEvent));
        }

        public void OnTag(ITagOperatorEvent tagEvent)
        {
            ForAll((h) => h.OnTag(tagEvent));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<IEventHandler> GetEnumerator()
        {
            return _handlers.GetEnumerator();
        }

        public void Add(IEventHandler handler)
        {
            _handlers.Add(handler);
        }
    }
}