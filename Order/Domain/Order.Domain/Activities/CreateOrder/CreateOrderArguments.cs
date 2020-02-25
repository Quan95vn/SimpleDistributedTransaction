using Contracts.Models;
using System;
using System.Collections.Generic;

namespace Order.Domain.Activities.CreateOrder
{
    public interface CreateOrderArguments
    {
        Guid OrderId { get; }

        string Address { get; }

        DateTime CreatedDate { get; }

        IEnumerable<OrderDetail> OrderDetails { get; }

        string ErrorMessage { get; set; }
    }
}