using System;
using System.Threading.Tasks;

namespace Order.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<Models.Order> GetByOrderId(Guid orderId);

        Task Add(Models.Order order);

        Task Update(Models.Order order);
    }
}