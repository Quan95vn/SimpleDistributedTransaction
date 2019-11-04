using System;

namespace Contracts
{
    /// <summary>
    /// Interface SubmitOrder
    /// </summary>
    public interface SubmitOrder
    {
        Guid OrderId { get; }

        DateTime CreatedDate { get; }
    }
}