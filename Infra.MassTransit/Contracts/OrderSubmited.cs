using Contracts.Models;
using MassTransit;
using System;
using System.Collections.Generic;

namespace Contracts
{
    /// <summary>
    /// Interface OrderSubmited
    /// </summary>
    public interface OrderSubmitted : CorrelatedBy<Guid>
    {
        Guid OrderId { get; }

        string ErrorMessage { get; }
    }
}
