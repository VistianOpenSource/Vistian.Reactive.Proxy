using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading;
using Vistian.Reactive.Proxy.Events;
using bcl = System.Reflection;

namespace Vistian.Reactive.Proxy.Utils
{
    public static class CallSiteCache
    {
        public static CallSite Get(int skipFrames)
        {
            // Account for ourselves
            return CallSiteMixins.FromStack(new StackFrame(skipFrames, true));
        }
    }
}
