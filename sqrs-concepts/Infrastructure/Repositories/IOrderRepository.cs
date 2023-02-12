using sqrs_concepts.Domain.Orders;

namespace sqrs_concepts.Infrastructure.Repositories
{
    public interface IOrderRepository
    {
        ValueTask<Order> SaveOrderAsync(Order order);
    }
}