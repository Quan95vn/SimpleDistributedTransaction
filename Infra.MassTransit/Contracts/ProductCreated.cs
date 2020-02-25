using MassTransit;
using System;

namespace Contracts
{
    /// <summary>
    /// Interface ProductCreated
    /// </summary>
    public interface ProductCreated  : CorrelatedBy<Guid>
    {
        Guid ProductId { get; }

        DateTime CreatedDate { get; }
    }
}