using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Vistian.Reactive.Proxy.EventHandlers.Snapshot.Local;
using Vistian.Reactive.Proxy.Events;

namespace Vistian.Reactive.Proxy.EventHandlers.Exceptions
{
    public class OnErrorExceptionEventArgs : System.EventArgs
    {
        public IOnErrorEvent ErrorEvent { get; }
        public State State1 { get; }

        public OnErrorExceptionEventArgs(IOnErrorEvent errorEvent,State state)
        {
            ErrorEvent = errorEvent;
            State1 = state;
        }

        // now, what do we want to produce?

        public string Formatted
        {
            get
            {
                var sb = new StringBuilder();

                sb.AppendLine($"Reactive Extensions Exception Observed: {ErrorEvent.ErrorType.Name} - {ErrorEvent.Message}");

                sb.AppendLine($"Pipeline backtrace:");

                var obs = State1.TrackedObservables.FirstOrDefault(p => p.Id == ErrorEvent.OperatorId);

                if (obs != null)
                {
                    foreach (var parent in EnumerateParents(obs, 1))
                    {
                        obs = parent.State;
                        sb.Append(' ', parent.Indent);
                        sb.AppendLine($"{obs.CallSite.Method.DeclaringType}.{obs.CallSite.Method.Signature} {obs.OperatorMethod.Signature}  {obs.CallSite.File};{obs.CallSite.Line}");
                    }
                }

                sb.AppendLine("Stack Trace:");
                sb.Append(ErrorEvent.StackTrace);

                return sb.ToString();
            }
        }


        internal class ParentMatch
        {
            public int Indent { get; set; }
            public ObservableState State { get; set; }

            public ParentMatch(int indent, ObservableState state)
            {
                Indent = indent;
                State = state;
            }
        }
        private IEnumerable<ParentMatch> EnumerateParents(ObservableState state,int level)
        {
            //if (model.Parents.Count == 0)
            {
                yield return new ParentMatch(level,state);
            }

            foreach (var parent in state.Parents)
            {

                foreach(var item in EnumerateParents(parent,level+1))
                {
                    yield return item;
                }
            }
        }
    }
}