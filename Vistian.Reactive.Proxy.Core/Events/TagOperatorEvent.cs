namespace Vistian.Reactive.Proxy.Events
{
    public class TagOperatorEvent : Event, ITagOperatorEvent
    {
        public long OperatorId { get; set; }
        public string Tag { get; set; }

        public TagOperatorEvent(OperatorInfo info, string tag)
            : base(EventType.TagOperator)
        {
            OperatorId = info.Id;
            Tag = tag;
        }
    }
}
