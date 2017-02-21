using System;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using Vistian.Reactive.Proxy.Events;
using Vistian.Reactive.Proxy.Extensions;

namespace Vistian.Reactive.Proxy.Observables
{
    public class ConnectableOperatorConnection<T> : OperatorConnection<T>, IConnectableObservable<T>
    {
        readonly IConnectableObservable<T> _connectableObservable;

        public ConnectableOperatorConnection(ISession session, IConnectableObservable<T> parent, OperatorInfo childInfo)
            : base(session, parent, childInfo)
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
