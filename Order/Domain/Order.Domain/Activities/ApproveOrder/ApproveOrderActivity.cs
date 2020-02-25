using MassTransit.Courier;
using Order.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Order.Domain.Activities.ApproveOrder
{
    public class ApproveOrderActivity : Activity<ApproveOrderArguments, ApproveOrderLog>
    {
        private readonly IOrderRepository _orderRepository;

        public ApproveOrderActivity(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ExecutionResult> Execute(ExecuteContext<ApproveOrderArguments> context)
        {
            var order = await _orderRepository.GetByOrderId(context.Arguments.OrderId);
            if (order == null)
                context.Terminate();

            order.SetApporveOrder();
            await _orderRepository.Update(order);

            return context.Completed(new Log(order.OrderId));
        }

        public async Task<CompensationResult> Compensate(CompensateContext<ApproveOrderLog> context)
        {
            context.Log.ErrorMessage = "abc";
            throw new NotImplementedException();
        }

        private class Log : ApproveOrderLog
        {
            public Log(Guid orderId)
            {
                OrderId = orderId;
            }

            public Guid OrderId { get; set; }
            public string ErrorMessage { get; set; }
        }
    }
}