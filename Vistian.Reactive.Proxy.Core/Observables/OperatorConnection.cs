using System;
using System.Reactive.Disposables;
using Vistian.Reactive.Proxy.Extensions;

namespace Vistian.Reactive.Proxy.Observables
{
    public class OperatorConnection<T> : IObservable<T>, IOperatorObservable, IConnection
    {
        readonly OperatorInfo _childInfo;
        private IObservable<T> _parent;
        private OperatorInfo _parentInfo;
        private ISession _session;

        public OperatorInfo OperatorInfo { get { return _childInfo; } }
        protected ISession Session { get { return _session; } }

        public OperatorConnection(ISession session, IObservable<T> parent, OperatorInfo childInfo)
        {
            _session = session;
            _parent = parent;

            var oobs = parent as IOperatorObservable;

            if (oobs != null)
                _parentInfo = oobs.OperatorInfo;

            _childInfo = childInfo;
        }

        public override string ToString()
        {
            return _childInfo.ToString() + "::Connection";
        }

        public virtual IDisposable Subscribe(IObserver<T> observer)
        {
            // Parent is not a tracked observable.
            if (_parentInfo == null)
            {
                return _parent.Subscribe(observer);
            }

            var subscriptionId = EventHandlerExtensions.OnSubscribe(_session, _childInfo, _parentInfo);

            var disp = _parent.Subscribe(observer);

            return Disposable.Create(() =>
            {
                disp.Dispose();
                EventHandlerExtensions.OnUnsubscribe(_session, subscriptionId);
            });
        }
    }
}
