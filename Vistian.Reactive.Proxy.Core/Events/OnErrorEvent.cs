﻿using System;

namespace Vistian.Reactive.Proxy.Events
{
    public class OnErrorEvent : Event, IOnErrorEvent
    {
        public ITypeInfo ErrorType { get; private set; }
        public string Message { get; private set; }
        public long OperatorId { get; private set; }
        public string StackTrace { get; private set; }

        public OnErrorEvent(OperatorInfo operatorInfo, Exception error)
            : base(EventType.OnError)
        {
            if (error == null)
                return;

            OperatorId = operatorInfo.Id;
            ErrorType = new TypeInfo(error.GetType());
            Message = error.Message;
            StackTrace = error.StackTrace;
        }
    }
}
