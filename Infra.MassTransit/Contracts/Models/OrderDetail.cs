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
        Guid ProductId { get; }

        int Quantity { get; }
    }
}
