using Order.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Interfaces
{
    public interface IOrderDetailRepository
    {
        Task Add(OrderDetail orderDetail);
    }
}
