namespace Vistian.Reactive.Proxy.Events
{
    public class CallSite : ICallSite
    {
        public int Line { get; private set; }
        public string File { get; private set; }
        public int IlOffset { get; }
        public int ILOffset { get; private set; }
        public IMethodInfo Method { get; private set; }

        public CallSite(int line, string file, int ilOffset, IMethodInfo method)
        {
            Line = line;
            File = file;
            IlOffset = ilOffset;
            Method = method;
        }
    }
}