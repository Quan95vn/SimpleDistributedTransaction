using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    /// <summary>
    /// Interface CreateProduct
    /// </summary>
    public interface CreateProduct : CorrelatedBy<Guid>
    {
        Guid ProductId { get; }

        string Name { get; }

        decimal Price { get; }

        int QuantityInStock { get; }

        DateTime CreatedDate { get; }
    }
}
