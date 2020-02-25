using Microsoft.Extensions.DependencyInjection;
using Order.Data.Context;
using Order.Data.Repository;
using Order.Domain.DomainHandler;
using Order.Domain.Interfaces;
using Product.Data.Context;
using Product.Data.Repository;
using Product.Domain.Interfaces;
using System;

namespace SimpleDistributedTransactio.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {

            //Data
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();

            services.AddTransient<ProductDbContext>();
            services.AddTransient<OrderDbContext>();


        }
    }
}
