using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task Add(Models.Order order);
    }
}
