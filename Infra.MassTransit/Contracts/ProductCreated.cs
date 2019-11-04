using System;

namespace Contracts
{
    /// <summary>
    /// Interface ProductCreated
    /// </summary>
    public interface ProductCreated
    {
        Guid ProductId { get; }

        DateTime CreatedDate { get; }
    }
}