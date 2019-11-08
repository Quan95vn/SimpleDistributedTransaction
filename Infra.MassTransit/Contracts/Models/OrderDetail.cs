using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Models
{
    /// <summary>
    /// Interface OrderDetail
    /// </summary>
    public interface OrderDetail
    {
        Guid OrderId { get; }

        Guid ProductId { get; }

        int Quantity { get; }
    }
}
