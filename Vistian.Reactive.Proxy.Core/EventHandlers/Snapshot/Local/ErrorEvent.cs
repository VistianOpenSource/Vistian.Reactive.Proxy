using System;
using Vistian.Reactive.Proxy.Events;

namespace Vistian.Reactive.Proxy.EventHandlers.Snapshot.Local
{
    public class ErrorEvent
    {
        public ITypeInfo ErrorType { get; set; }
        public string Message { get; set; }
        public TimeSpan Received { get; set; }
        public string StackTrace { get; set; }

        public ErrorEvent(IOnErrorEvent onErrorEvent)
        {
            Received = TimeSpan.FromMilliseconds(onErrorEvent.EventTime);
            ErrorType = onErrorEvent.ErrorType;
            Message = onErrorEvent.Message;
            StackTrace = onErrorEvent.StackTrace;
        }

    }
}
