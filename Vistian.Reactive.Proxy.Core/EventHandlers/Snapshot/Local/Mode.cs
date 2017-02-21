using System;

namespace Vistian.Reactive.Proxy.EventHandlers.Snapshot.Local
{
    [Flags]
    public enum Mode
    {
        CallSite=0x01,
        Values=0x02,
        All = CallSite | Values,
        None=0x00
    };
}