using System;

namespace Contracts
{
    /// <summary>
    /// Interface OrderSubmited
    /// </summary>
    public interface OrderSubmited
    {
        Guid OrderId { get; }

        DateTime CreatedDate { get; }
    }
}
