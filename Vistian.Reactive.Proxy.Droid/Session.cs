using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reflection;
using System.Threading;
using Vistian.Reactive.Proxy;
using Vistian.Reactive.Proxy.Events;


namespace Vistian.Reactive.Proxy
{
    /// <summary>
    ///     Main class for tracing of Rx activities.
    /// </summary>
    public class Session : ISession, IEventHandler, IDisposable
    {
        private static int _launched;
        private readonly IEventHandler _eventHandler;
        private readonly bool _explicitCapture;

        [ThreadStatic] private bool _isInCaptureScope;

        public bool IsCapturing => _explicitCapture == false || _isInCaptureScope;

        /// <summary>
        ///     Get the session
        /// </summary>
        internal static Session Current { get; private set; }

        /// <summary>
        ///     Create an instance with a specified event handler.
        /// </summary>
        /// <param name="eventHandler"></param>
        /// <param name="explicitCapture"></param>
        private Session(IEventHandler eventHandler, bool explicitCapture)
        {
            _eventHandler = eventHandler;
            _explicitCapture = explicitCapture;
        }

        /// <summary>
        ///     Create a session,
        /// </summary>
        /// <param name="eventHandler"></param>
        /// <param name="explicitCapture"></param>
        /// <returns></returns>
        public static Session Create(IEventHandler eventHandler, bool explicitCapture = false)
        {
            var session = new Session(eventHandler, explicitCapture);

            Current = session;
            RxTraceContext.Current = session;

            if (Interlocked.CompareExchange(ref _launched, 1, 0) != 0)
                throw new InvalidOperationException("Session already created");

            InstallInterceptingQueryLanguage(session);

            return session;
        }

        private static object _originalInterceptor;
        private static object _trackingInterceptor;
        private static FieldInfo _interceptorField;

        /// <summary>
        ///     Install the proxy/interceptor through which all rx calls will be caught.
        /// </summary>
        /// <param name="session"></param>
        private static void InstallInterceptingQueryLanguage(Session session)
        {
            // Make sure it's initialized
            Observable.Empty<Unit>();

            var observableType = typeof(Observable);

            _interceptorField = observableType.GetField("s_impl", BindingFlags.Static | BindingFlags.NonPublic);

            _originalInterceptor = _interceptorField.GetValue(null);

            _trackingInterceptor = new QueryLanguageProxy(session, _originalInterceptor).GetTransparentProxy();

            EnableInterceptor(true);
        }

        private static void EnableInterceptor(bool enable)
        {
            _interceptorField.SetValue(null, enable ? _trackingInterceptor : _originalInterceptor);
        }

        public static IDisposable Capture()
        {
            Current.StartCapture();
            return Disposable.Create(() => Current.StopCapture());
        }

        public void StartCapture()
        {
            _isInCaptureScope = true;
        }

        public void StopCapture()
        {
            _isInCaptureScope = false;
        }

        public void OnCreated(IOperatorCreatedEvent onCreatedEvent)
        {
            _eventHandler.OnCreated(onCreatedEvent);
        }

        public void OnCompleted(IOnCompletedEvent onCompletedEvent)
        {
            _eventHandler.OnCompleted(onCompletedEvent);
        }

        public void OnError(IOnErrorEvent onErrorEvent)
        {
            _eventHandler.OnError(onErrorEvent);
        }

        public void OnNext(IOnNextEvent onNextEvent)
        {
            _eventHandler.OnNext(onNextEvent);
        }

        public void OnSubscribe(ISubscribeEvent subscribeEvent)
        {
            _eventHandler.OnSubscribe(subscribeEvent);
        }

        public void OnUnsubscribe(IUnsubscribeEvent unsubscribeEvent)
        {
            _eventHandler.OnUnsubscribe(unsubscribeEvent);
        }

        public void OnConnected(IConnectedEvent connectedEvent)
        {
            _eventHandler.OnConnected(connectedEvent);
        }

        public void OnDisconnected(IDisconnectedEvent disconnectedEvent)
        {
            _eventHandler.OnDisconnected(disconnectedEvent);
        }

        public void OnTag(ITagOperatorEvent tagEvent)

        {
            _eventHandler.OnTag(tagEvent);
        }

        public void Dispose()
        {
            _eventHandler.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
