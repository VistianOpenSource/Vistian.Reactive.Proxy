namespace Vistian.Reactive.Proxy
{
    public interface ISession:IEventHandler
    {        
    }

    public static class RxTraceContext
    {
        public static ISession Current { get; set; }
    }
}