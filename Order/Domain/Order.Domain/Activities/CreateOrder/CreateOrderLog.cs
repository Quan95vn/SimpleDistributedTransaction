using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Activities.CreateOrder
{
    public interface CreateOrderLog
    {
        Guid OrderId { get; }
    }
}
