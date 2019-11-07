using System;
using System.Collections.Generic;
using System.Text;

namespace Masstransit.Activities.CreateOrder
{
    public interface CreateOrderLog
    {
        Guid OrderId { get; }
    }
}
