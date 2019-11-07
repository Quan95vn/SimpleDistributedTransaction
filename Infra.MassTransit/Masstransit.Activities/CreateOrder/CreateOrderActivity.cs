using MassTransit.Courier;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Masstransit.Activities.CreateOrder
{
    public class CreateOrderActivity : Activity<CreateOrderArguments, CreateOrderLog>
    {
        //private readonly IOrderRepository _orderRepository;
        //private readonly IOrderDetailRepository _orderDetailRepository;

        public CreateOrderActivity()
        {

        }

        public async Task<ExecutionResult> Execute(ExecuteContext<CreateOrderArguments> context)
        {



            return context.Completed(new Log(context.Arguments.OrderId));
        }

        public async Task<CompensationResult> Compensate(CompensateContext<CreateOrderLog> context)
        {

            return context.Compensated();
        }

        class Log : CreateOrderLog
        {
            public Log(Guid orderId)
            {
                OrderId = orderId;
            }

            public Guid OrderId { get; }
        }
    }
}
