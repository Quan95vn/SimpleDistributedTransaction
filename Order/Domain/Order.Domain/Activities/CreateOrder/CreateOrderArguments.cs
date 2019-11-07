using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Activities.CreateOrder
{
    public interface CreateOrderArguments
    {
        Guid OrderId { get; }

        string Address { get; }

        DateTime CreatedDate { get; }
    }
}
