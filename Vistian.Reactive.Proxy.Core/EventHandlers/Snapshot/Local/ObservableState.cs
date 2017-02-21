using System;
using System.Collections.Generic;
using System.Linq;
using Vistian.Reactive.Proxy.Events;

namespace Vistian.Reactive.Proxy.EventHandlers.Snapshot.Local
{
    public class ObservableState
    {
        private const string ActiveState = "Active";
        private const string CompletedState = "Completed";
        private const string ErrorState = "Error";

        public long Id { get; set; }
        public string Name { get; set; }

        public IMethodInfo OperatorMethod { get; private set; }
        public ICallSite CallSite { get; private set; }

        public string Tag { get; set; }

        public TimeSpan Created { get; private set; }

        public List<ObservableState> Parents { get; set; }
        
        public List<ObservableState> Children { get; set; }

        public List<Subscription> Subscriptions { get; set; }

        public List<ObservedValue> ObservedValues { get; set; }

        public ErrorEvent Error { get; set; }

        public bool HasError { get; set; }

        public bool IsActive { get; set; }

        public long ValuesProduced { get; set; }

        public int Descendants {
            get
            {
                return Parents.Select(c => c.Children.Contains(this)).ToList().Count;
            } }

        public int Ancestors
        {
            get { return Parents.Select(c => c.Parents.Contains(this)).ToList().Count; }
        }

        public string Status { get; private set; }

        public ObservableState(IOperatorCreatedEvent createdEvent)
        {
            Id = createdEvent.Id;
            Name = createdEvent.Name;
            OperatorMethod = createdEvent.OperatorMethod;
            CallSite = createdEvent.CallSite;
            IsActive = true;

            Created = TimeSpan.FromMilliseconds(createdEvent.EventTime);

            Subscriptions = new List<Subscription>();
            Parents = new List<ObservableState>();
            Children = new List<ObservableState>();

            Status = ActiveState;
        }

        public void OnNext(IOnNextEvent onNextEvent)
        {
            if (ObservedValues == null)
            {
                ObservedValues = new List<ObservedValue>();
            }

            ObservedValues.Add(new ObservedValue(onNextEvent));
            ValuesProduced++;
        }

        public void OnCompleted(IOnCompletedEvent onCompletedEvent)
        {
            IsActive = false;
            Status = CompletedState;
        }

        public void OnError(IOnErrorEvent onErrorEvent)
        {
            Error = new ErrorEvent(onErrorEvent);
            IsActive = false;
            HasError = true;

            Status = ErrorState;
        }

        public void OnTag(ITagOperatorEvent onTagEvent)
        {
            Tag = onTagEvent.Tag;
        }
    }
}
