using Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using Product.Domain.Interfaces;
using System.Threading.Tasks;

namespace Product.Domain.Consumers
{
    public class CreateProductConsumer : IConsumer<CreateProduct>
    {
        private readonly ILogger<CreateProductConsumer> _log;
        private readonly IProductRepository _productRepository;

        public CreateProductConsumer(ILoggerFactory loggerFactory, IProductRepository productRepository)
        {
            _log = loggerFactory.CreateLogger<CreateProductConsumer>();
            _productRepository = productRepository;
        }

        public async Task Consume(ConsumeContext<CreateProduct> context)
        {
            var product = new Models.Product
            (
                context.Message.ProductId,
                context.Message.Name,
                context.Message.Price,
                context.Message.QuantityInStock,
                context.Message.CreatedDate
            );
            await _productRepository.Add(product);

            await context.RespondAsync<ProductCreated>(new
            {
                context.Message.ProductId,
                context.Message.CreatedDate
            });

            _log.LogInformation("Product created {ProductId}", context.Message.ProductId);
        }
    }
}