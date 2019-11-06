using Contracts.Models;

namespace Masstransit.Activities.ApproveOrder
{
    public interface ApproveOrderArguments
    {
        Order Order { get; }
    }
}
