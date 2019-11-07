using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Domain.Activities.ReserveProduct
{
    public interface ReserveProductArguments
    {
       IEnumerable<OrderDetail> OrderDetails { get; }
    }
}
