using System.Diagnostics;
using Vistian.Reactive.Proxy.Events;

namespace Vistian.Reactive.Proxy.Events
{
    public static class CallSiteMixins
    {
        public static CallSite FromStack(StackFrame frame)
        {
            var line = frame.GetFileLineNumber();
            var file = frame.GetFileName();
            var ilOffset = frame.GetILOffset();

            var method = frame.GetMethod();

            MethodInfo methodInfo=null;
            if (method != null)
                methodInfo = new MethodInfo(method);

            return new CallSite(line,file,ilOffset,methodInfo);
        }
    }
}
