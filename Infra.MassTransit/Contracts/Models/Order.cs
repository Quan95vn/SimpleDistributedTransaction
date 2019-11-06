using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Models
{
    public interface Order
    {
        Guid OrderId { get; }

        string Address { get; }

        IEnumerable<OrderDetail> OrderDetails { get; }
    }
}
