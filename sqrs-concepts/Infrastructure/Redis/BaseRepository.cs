using System;
using sqrs_concepts.Domain;
using StackExchange.Redis;

namespace sqrs_concepts.Infrastructure.Redis
{
	public abstract class BaseRepository
	{
        private IConnectionMultiplexer _redisConnection;

        public BaseRepository(IConnectionMultiplexer redisConnection)
		{
            _redisConnection = redisConnection;
		}

		public async ValueTask<bool> SaveAggregateAsync(AggregateBase aggregate)
		{
            var db = _redisConnection.GetDatabase();
            var tran = db.CreateTransaction();

            // Save order as a hash
            var key = $"order:{aggregate.Id}";
            var hash = new HashEntry[]
            {
                new HashEntry("Id", aggregate.Id.ToString()),
                // new HashEntry("Customer", order.Customer),
                // new HashEntry("Total", order.Total)
            };
            tran.HashSetAsync(key, hash);

            // Emit OrderPlaced event to RedisStream
            var message = new NameValueEntry[]
            {
                new NameValueEntry("OrderId", aggregate.Id.ToString()),
                new NameValueEntry("EventType", "OrderPlaced")
            };
            tran.StreamAddAsync("OrdersPlaced", message);

            // Execute the transaction
            return await tran.ExecuteAsync();
        }
	}
}

