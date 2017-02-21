using System;
using Vistian.Reactive.Proxy.Utils;

namespace Vistian.Reactive.Proxy.Events
{
    public class OnNextEvent : Event, IOnNextEvent
    {
        public long OperatorId { get; private set; }
        public string ValueType { get; private set; }
        public string Value { get; private set; }
        public int Thread { get; private set; }

        public OnNextEvent(OperatorInfo operatorInfo, Type valueType, object value, int thread)
            : base(EventType.OnNext)
        {
            OperatorId = operatorInfo.Id;
            ValueType = TypeUtils.ToFriendlyName(valueType);
            Value = ValueFormatter.ToString(value, valueType);
        }
    }
}
