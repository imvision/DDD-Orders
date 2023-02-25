using System;
namespace sqrs_concepts.Domain.Orders
{
    public interface IOrderRepository
    {
        ValueTask<Order> SaveAsync(Order order);
    }
}

