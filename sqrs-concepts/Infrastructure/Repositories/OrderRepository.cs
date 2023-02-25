using System;
using sqrs_concepts.Domain.Orders;
using sqrs_concepts.Infrastructure.Streams;

namespace sqrs_concepts.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public OrderRepository(IEventWriter eventWriter)
            : base(eventWriter)
        {
        }

        public async ValueTask<Order> SaveAsync(Order order)
        {
            var sql = @"Insert into Orders (Id, MarketId, SelectionId, Price, Size, Side, PlaceTime, Version)
				Values (@Id, @MarketId, @SelectionId, @Price, @Size, @Side, @PlaceTime, @Version)";

            await SaveAggregateAsync(sql, order);

            return order;
        }
    }
}
