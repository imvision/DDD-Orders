namespace sqrs_concepts.Domain
{
    public abstract class Event
    {
        public Guid Id { get; }

        public int Version { get; }

        public DateTime Timestamp { get; }

        protected Event(Guid id, int version)
        {
            Id = id;
            Version = version;
            Timestamp = DateTime.UtcNow;
        }
    }
}
