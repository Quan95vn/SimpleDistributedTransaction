using System;
using System.Collections.Generic;
using System.Text;

namespace Masstransit.Activities.ApproveOrder
{
    public interface ApproveOrderLog
    {
        string TransactionId { get; }

        Guid MessageId { get; }

        Guid OrderId { get; }
    }
}
