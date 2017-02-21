using System;
using System.Collections.Concurrent;
using System.Reflection;
using Vistian.Reactive.Proxy;
using Vistian.Reactive.Proxy.Observables;

namespace Vistian.Reactive.Proxy.Utils
{
    public static class OperatorFactory
    {
        static readonly ConcurrentDictionary<Type, Lazy<ConstructorInfo>> ConnectionConstructorCache = new ConcurrentDictionary<Type, Lazy<ConstructorInfo>>();


        public static object CreateOperatorObservable(object source, Type signalType, OperatorInfo operatorInfo)
        {
            var ctor = ConnectionConstructorCache.GetOrAdd(
                signalType,
                _ => new Lazy<ConstructorInfo>(() => GetOperatorConstructor(signalType)));

            return ctor.Value.Invoke(new object[] { Session.Current, source, operatorInfo });
        }

        static ConstructorInfo GetOperatorConstructor(Type signalType)
        {
            var operatorObservable = typeof(OperatorObservable<>).MakeGenericType(signalType);

            return operatorObservable.GetConstructor(new[] { 
                typeof(Session), 
                typeof(IObservable<>).MakeGenericType(signalType), 
                typeof(OperatorInfo) 
            });
        }
    }
}
