using Contracts.Models;
using System;
using System.Collections.Generic;

namespace Contracts
{
    /// <summary>
    /// Interface OrderSubmited
    /// </summary>
    public interface OrderSubmitted
    {
        Guid OrderId { get; }
    }
}
