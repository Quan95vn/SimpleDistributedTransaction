using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    /// <summary>
    /// Interface CreateProduct
    /// </summary>
    public interface CreateProduct
    {
        Guid ProductId { get; }

        DateTime CreatedDate { get; }
    }
}
