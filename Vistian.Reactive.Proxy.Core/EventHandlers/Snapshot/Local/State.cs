using System.Collections.Concurrent;
using System.Collections.Generic;
using Vistian.Reactive.Proxy.Events;

namespace Vistian.Reactive.Proxy.EventHandlers.Snapshot.Local
{
    public class State 
    {
        private readonly Mode _mode;

        readonly ConcurrentDictionary<long, ObservableState> _observableRepository
            = new ConcurrentDictionary<long, ObservableState>();

        readonly ConcurrentDictionary<long, Subscription> _subscriptionRepository
            = new ConcurrentDictionary<long, Subscription>();

        public List<ObservableState> TrackedObservables { get; set; }

        public long SignalCount { get; set; }

        public long ErrorCount { get; set; }

        public State(Mode mode=Mode.All)
        {
            _mode = mode;

            TrackedObservables = new List<ObservableState>();
        }

        internal void OnEvent(IEvent ev)
        {
            switch (ev.EventType)
            {
                case EventType.OperatorCreated:
                    OnOperatorCreated((IOperatorCreatedEvent)ev);
                    break;

                case EventType.Subscribe:
                    OnSubscribe((ISubscribeEvent)ev);
                    break;

                case EventType.Unsubscribe:
                    OnUnsubscribe((IUnsubscribeEvent)ev);
                    break;

                case EventType.OnCompleted:
                    OnCompleted((IOnCompletedEvent)ev);
                    break;

                case EventType.OnNext:
                    OnNext((IOnNextEvent)ev);
                    break;

                case EventType.OnError:
                    OnError((IOnErrorEvent)ev);
                    break;

                case EventType.TagOperator:
                    OnTag((ITagOperatorEvent)ev);
                    break;
            }
        }

        void OnOperatorCreated(IOperatorCreatedEvent operatorCreatedEvent)
        {
            var operatorModel = new ObservableState(operatorCreatedEvent);

            _observableRepository.TryAdd(operatorCreatedEvent.Id, operatorModel);
            TrackedObservables.Add(operatorModel);
        }

        void OnSubscribe(ISubscribeEvent subscribeEvent)
        {
            ObservableState child, parent;

            _observableRepository.TryGetValue(subscribeEvent.ChildId, out child);
            _observableRepository.TryGetValue(subscribeEvent.ParentId, out parent);

            var subscriptionModel = new Subscription(subscribeEvent, child, parent)
            {
                IsActive = true
            };

            _subscriptionRepository.TryAdd(subscribeEvent.EventId, subscriptionModel);

            // hang on, this doesn't quite work, we need to 
            // the values all the way up ?

            if (parent != null)
            {
                parent.Subscriptions.Add(subscriptionModel);

                parent.Children.Add(child);

                child?.Parents.Add(parent);
            }
        }

        void OnUnsubscribe(IUnsubscribeEvent unsubscribeEvent)
        {
            Subscription subscription;

            _subscriptionRepository.TryGetValue(unsubscribeEvent.SubscriptionId, out subscription);

            if (subscription != null)
            {
                subscription.IsActive = false;
            }
        }

        private void OnError(IOnErrorEvent onErrorEvent)
        {
            ErrorCount++;

            ObservableState operatorState;
            _observableRepository.TryGetValue(onErrorEvent.OperatorId, out operatorState);

            operatorState.OnError(onErrorEvent);
        }

        private void OnNext(IOnNextEvent onNextEvent)
        {
            SignalCount++;

            if ((_mode & Mode.Values) != Mode.None)
            {
                ObservableState operatorState;
                _observableRepository.TryGetValue(onNextEvent.OperatorId, out operatorState);

                operatorState.OnNext(onNextEvent);
            }
        }

        void OnCompleted(IOnCompletedEvent onCompletedEvent)
        {
            ObservableState operatorState;
            _observableRepository.TryGetValue(onCompletedEvent.OperatorId, out operatorState);

            operatorState?.OnCompleted(onCompletedEvent);
        }

        private void OnTag(ITagOperatorEvent tagOperatorEvent)
        {
            ObservableState operatorState;
            _observableRepository.TryGetValue(tagOperatorEvent.OperatorId, out operatorState);

            operatorState?.OnTag(tagOperatorEvent);
        }
    }
}
