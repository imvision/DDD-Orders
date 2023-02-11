using System.Text.Json;
using sqrs_concepts.Domain;
using StackExchange.Redis;

namespace sqrs_concepts.Infrastructure.Streams
{
    public class RedisEventWriter : IEventWriter
    {
        private IConnectionMultiplexer _redisConnection;

        public RedisEventWriter(IConnectionMultiplexer connection)
        {
            _redisConnection = connection;
        }

        public async Task WriteEvent(Event eventData)
        {
            var topic = eventData.GetType().Name;
            var json = JsonSerializer.Serialize(eventData);
            await _redisConnection.GetDatabase().StreamAddAsync(topic, "event", json);
        }
    }
}
