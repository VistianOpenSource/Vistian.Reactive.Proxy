using System;
using Vistian.Reactive.Proxy.Events;

namespace Vistian.Reactive.Proxy.EventHandlers.Snapshot.Local
{
    public class ObservedValue
    {
        public string ValueType { get; set; }
        public string Value { get; set; }
        public TimeSpan Received { get; set; }
        public int Thread { get; set; }

        public ObservedValue(IOnNextEvent onNextEvent)
        {
            ValueType = onNextEvent.ValueType;
            Value = onNextEvent.Value;
            Thread = onNextEvent.Thread;

            Received = TimeSpan.FromMilliseconds(onNextEvent.EventTime);
        }

    }
}
