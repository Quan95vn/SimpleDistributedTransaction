using System;

namespace Order.Domain.Activities.ApproveOrder
{
    public interface ApproveOrderLog
    {
        Guid OrderId { get; }
    }
}