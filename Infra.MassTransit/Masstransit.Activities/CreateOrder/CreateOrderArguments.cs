using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masstransit.Activities.CreateOrder
{
    public interface CreateOrderArguments
    {
        Guid OrderId { get; }

        string Address { get; }

        DateTime CreatedDate { get; }
    }
}
