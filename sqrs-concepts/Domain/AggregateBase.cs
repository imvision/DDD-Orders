namespace sqrs_concepts.Domain
{
    public abstract class AggregateBase
    {
        private readonly List<Event> _events = new List<Event>();

        public Guid Id { get; protected set; }

        public int Version { get; protected set; }

        public IReadOnlyCollection<Event> GetEvents()
        {
            return _events.AsReadOnly();
        }

        protected void ApplyEvent(Event @event)
        {
            Version++;
            _events.Add(@event);
        }
    }
}
