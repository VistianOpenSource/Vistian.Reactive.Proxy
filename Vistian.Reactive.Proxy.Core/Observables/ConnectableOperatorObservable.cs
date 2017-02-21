using System;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using Vistian.Reactive.Proxy.Events;
using Vistian.Reactive.Proxy.Extensions;

namespace Vistian.Reactive.Proxy.Observables
{
    public class ConnectableOperatorObservable<T> : OperatorObservable<T>, IConnectableObservable<T>
    {
        readonly IConnectableObservable<T> _connectableObservable;

        public ConnectableOperatorObservable(ISession session, IConnectableObservable<T> parent, OperatorInfo operatorInfo)
            : base(session, parent, operatorInfo)
        {
            _connectableObservable = parent;
        }

        public IDisposable Connect()
        {
            var connectionId = EventHandlerExtensions.OnConnected(Session, OperatorInfo);
            var disp = _connectableObservable.Connect();

            return Disposable.Create(() =>
            {
                disp.Dispose();
                Session.OnDisconnected(Event.Disconnect(connectionId));
            });
        }
    }
}
