using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Domain.Activities.ReserveProduct
{
    public interface ReserveProductLog
    {
        IEnumerable<Guid> ProductId { get; }
    }
}
