using sqrs_concepts.Domain;

namespace sqrs_concepts.Infrastructure.Streams
{
    public interface IEventWriter
    {
        Task WriteEvent(Event eventData);
    }
}
