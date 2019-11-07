using MassTransit.Courier;
using Order.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Activities.CreateOrder
{
    public class CreateOrderActivity : Activity<CreateOrderArguments, CreateOrderLog>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;

        //public CreateOrderActivity(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        //{
        //    _orderRepository = orderRepository;
        //    _orderDetailRepository = orderDetailRepository;
        //}

        public async Task<ExecutionResult> Execute(ExecuteContext<CreateOrderArguments> context)
        {
            await _orderRepository.Add(
                new Models.Order(
                    context.Arguments.OrderId,
                    context.Arguments.Address,
                    context.Arguments.CreatedDate
                ));

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
