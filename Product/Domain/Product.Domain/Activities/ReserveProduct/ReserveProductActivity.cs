using Contracts;
using MassTransit.Courier;
using MassTransit.Courier.Exceptions;
using Product.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Domain.Activities.ReserveProduct
{
    public class ReserveProductActivity : Activity<ReserveProductArguments, ReserveProductLog>
    {
        private readonly IProductRepository _productRepository;

        public ReserveProductActivity(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ExecutionResult> Execute(ExecuteContext<ReserveProductArguments> context)
        {
            var orderDetails = context.Arguments.OrderDetails;

            foreach (var orderDetail in orderDetails)
            {
                var product = await _productRepository.GetByProductId(orderDetail.ProductId);
                if (product == null) continue;

                var quantity = product.SetQuantity(product.QuantityInStock - orderDetail.Quantity);
                
                if (quantity < 0)
                {
                    var errorMessage = "Out of stock.";
                    await context.Publish<ProcessOrder>(new
                    {
                        ErrorMessage = errorMessage
                    });
                    throw new RoutingSlipException(errorMessage);
                }

                await _productRepository.Update(product);
            }

            return context.Completed(new Log(orderDetails.Select(x => x.ProductId).ToList()));
        }

        public async Task<CompensationResult> Compensate(CompensateContext<ReserveProductLog> context)
        {
            return context.Compensated();
        }

        private class Log : ReserveProductLog
        {
            public Log(IEnumerable<Guid> productId)
            {
                ProductId = productId;
            }

            public IEnumerable<Guid> ProductId { get; set; }
        }
    }
}