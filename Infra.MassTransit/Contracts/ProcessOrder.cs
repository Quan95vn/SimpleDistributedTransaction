using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    /// <summary>
    /// Interface OrderCreated
    /// </summary>
    public interface ProcessOrder
    {
        Guid OrderId { get; }

        string Address { get; }

        IEnumerable<OrderDetail> OrderDetails { get; }

        DateTime CreatedDate { get; }

        string ErrorMessage { get; set; }
    }
}
