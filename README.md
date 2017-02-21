# Vistian.Reactive.Proxy

## Introduction
 
 A proxy allowing for the analysis of activity within solutions based on Reactive Extensions. 
## So why do we need a proxy?
During the development cycle, it can be difficult to diagnose the behaviour of solutions which use Reactive Extensions. This proxy is intended to provide a window on what is happening under the hood, allowing one to do things like:

* Analyse in more detail when exceptions occur
* Trace the flow of values through all of your reactive pipelines
* Determine which subscriptions haven't been disposed.

## Terminology

* **Event Handler** - Acts on the 'events' raised by the proxy.
* **Session** - The 'period' in which all Reactive Extensions activity is channelled through custom event handlers

## How do i use it?

1. Install the **pre-release** nuget package , Vistian.Reactive.Proxy.Droid from the MyGet feed https://www.myget.org/F/vistian-ci/api/v3/index.json 
2. At the start of the world in your application, add the code to create your 'event handlers' and install the proxy:

```
// Create the location used to hold all reactive observables state data
var sessionState = new Vistian.Reactive.Proxy.EventHandlers.Snapshot.Local.State();

// create handler to channel reactive events to the state 
var stateHandler = new Vistian.Reactive.Proxy.EventHandlers.Snapshot.Local.Handler(sessionState);

// create error handler
var errorHandler = new Vistian.Reactive.Proxy.EventHandlers.Exceptions.Handler(sessionState);

// setup event handler to receive exception events.
errorHandler.ErrorRaised += ErrorHandler_ErrorRaised;

// create aggregate container for exceptions
var handlers = new Vistian.Reactive.Proxy.EventHandlers.Aggregate.Handler(){ stateHandler,errorHandler};

// and finally create the session , we are off and running
var proxySession = Vistian.Reactive.Proxy.Session.Create(handlers);
```

3. Do 'something' within the exception event raised, e.g.


```
private void ErrorHandler_ErrorRaised(object sender, Vistian.Reactive.Proxy.EventHandlers.Exceptions.OnErrorExceptionEventArgs e)
{
    Console.WriteLine(e.Formatted);
}
```

4. Sit back and see watch data for observed exceptions that have been raised:

```
Reactive Extensions Exception Observed: InvalidOperationException - Operation is not valid due to the current state of the object.
Pipeline backtrace:
 MainActivity.OnCreate (Bundle bundle) Select<long, string> (IObservable<> source, Func<> selector)  E:\Temp\RxSpyAttempt\RxSpyAttempt\MainActivity.cs;86
  MainActivity.OnCreate (Bundle bundle) Select<long, long> (IObservable<> source, Func<> selector)  E:\Temp\RxSpyAttempt\RxSpyAttempt\MainActivity.cs;86
   MainActivity.OnCreate (Bundle bundle) Interval (TimeSpan period)  E:\Temp\RxSpyAttempt\RxSpyAttempt\MainActivity.cs;86
Stack Trace:
  at RxSpyAttempt.MainActivity+<>c.<OnCreate>b__6_3 (System.Int64 v) [0x0000b] in E:\Temp\RxSpyAttempt\RxSpyAttempt\MainActivity.cs:91 
  at System.Reactive.Linq.ObservableImpl.Select`2+_[TSource,TResult].OnNext (TSource value) [0x00008] in <d0067ed104ac455987b6feb85f80156b>:0 
```

which is what one sees for this rather simplistic example when an exception is raised:

```
Observable.Interval(TimeSpan.FromSeconds(1)).
    Select(v => v).
    Select(v =>
    {
        if (v > 10)
            throw new InvalidOperationException();

        return v.ToString();
    }).
    Subscribe(vs =>
    {
        Console.WriteLine(vs);
    });
```

## How does it work?
This code leans very heavily on the excellent work in RxSpy https://github.com/niik/RxSpy . 

In essence, all activity within Reactive Extensions is channelled through a single point. So... this single point is replaced with a proxy and then all calls are forwarded to the original implementation. However this single point isn't public and so a RealProxy is required to do this. The requirement for a RealProxy however does constrain this solution to not currently working on iOS. 

Once the calls are proxied, it then becomes a matter of deciding what to do with them. Currently the code contains two event handlers:

* State - which records all events into a single model. Options exist to also record every single value each observable sees as well.
* Exception - a handler which raises events whenever an OnError occurs.

It should be relatively easy to create ones own handlers, e.g. console output for all Reactive Events, or a remote service showing all the activity (as RxSpy does).

## Caveats

1. This code hasn't had a lot of testing.
2. Memory consumption on Android of this library hasn't been tested.
3. Performance of solutions will be effected with this library, don't deploy it in a live solution.
4. The code is a work in progress and should be seen as such. Functionality, along with class naming will more than likely evolve.



 