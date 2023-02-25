using System;
using sqrs_concepts.Domain.Orders;

namespace sqrs_concepts.Infrastructure.Redis
{
	public class OrderRepository : BaseRepository, IOrderRepository
    {
        public OrderRepository(StackExchange.Redis.IConnectionMultiplexer redisConnection)
            :base (redisConnection)
		{
		}

        public async ValueTask<Order> SaveAsync(Order order)
		{
            await SaveAggregateAsync(order);
            return order;
		}
    }
}

