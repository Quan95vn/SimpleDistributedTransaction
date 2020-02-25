using MassTransit;
using System;

namespace Contracts
{
    /// <summary>
    /// Interface SubmitOrder
    /// </summary>
    public interface SubmitOrder : CorrelatedBy<Guid>
    {
        Guid OrderId { get; }

        DateTime CreatedDate { get; }

        string Address { get; }
    }
}