using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface OrderRejected : CorrelatedBy<Guid>
    {
        Guid OrderId { get; }

        string ErrorMessage { get; }
    }
}
