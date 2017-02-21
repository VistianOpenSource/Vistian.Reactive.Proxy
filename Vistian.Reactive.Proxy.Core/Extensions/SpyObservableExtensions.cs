using System;
using Vistian.Reactive.Proxy.Observables;

namespace Vistian.Reactive.Proxy.Extensions
{
    public static class SpyObservableExtensions
    {
        public static IObservable<T> SpyTag<T>(this IObservable<T> source, string tag)
        {
            var oobs = source as OperatorObservable<T>;

            oobs?.Tag(tag);

            return source;
        }
    }
}
