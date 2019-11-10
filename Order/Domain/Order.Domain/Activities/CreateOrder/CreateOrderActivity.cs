using MassTransit;
using MassTransit.Courier;
using Order.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Order.Domain.Activities.CreateOrder
{
    public class CreateOrderActivity : Activity<CreateOrderArguments, CreateOrderLog>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;

        public CreateOrderActivity(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<ExecutionResult> Execute(ExecuteContext<CreateOrderArguments> context)
        {
            var order = context.Arguments;

            await _orderRepository.Add
            (
                new Models.Order(
                    order.OrderId,
                    order.Address,
                    order.CreatedDate
                )
            );

            foreach (var orderDetail in order.OrderDetails)
            {
                var model = new Models.OrderDetail
                (
                    NewId.NextGuid(),
                    order.OrderId,
                    orderDetail.ProductId,
                    orderDetail.Quantity
                );

                await _orderDetailRepository.Add(model);
            }


            return context.Completed(new Log(order.OrderId));
        }

        public async Task<CompensationResult> Compensate(CompensateContext<CreateOrderLog> context)
        {
            var order = await _orderRepository.GetByOrderId(context.Log.OrderId);
            order.SetCanceledOrder();
            await _orderRepository.Update(order);

            return context.Compensated();
        }

        private class Log : CreateOrderLog
        {
            public Log(Guid orderId)
            {
                OrderId = orderId;
            }

            public Guid OrderId { get; }
        }
    }
}