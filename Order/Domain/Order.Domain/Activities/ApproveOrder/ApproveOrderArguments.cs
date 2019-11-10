using System;

namespace Order.Domain.Activities.ApproveOrder
{
    public interface ApproveOrderArguments
    {
        Guid OrderId { get; }
    }
}