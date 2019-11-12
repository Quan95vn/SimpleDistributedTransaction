using Contracts;
using MassTransit;
using MassTransit.BusConfigurators;
using MassTransit.Configuration;
using MassTransit.Definition;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Order.Data.Context;
using Order.Data.Repository;
using Order.Domain.Activities.ApproveOrder;
using Order.Domain.Activities.CreateOrder;
using Order.Domain.Consumers;
using Order.Domain.Interfaces;
using Order.Domain.Proxies;
using Product.Data.Context;
using Product.Domain.Activities.ReserveProduct;
using Product.Domain.Consumers;
using Product.Domain.Interfaces;
using SimpleDistributedTransactio.Infra.IoC;
using System;
using System.Threading.Tasks;

namespace Console.Service
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", true);
                    config.AddEnvironmentVariables();

                    if (args != null)
                        config.AddCommandLine(args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var productDbConnectionString = hostContext.Configuration.GetSection("ConnectionStrings").GetSection("ProductDbConnection").Value;
                    var orderDbConnectionString = hostContext.Configuration.GetSection("ConnectionStrings").GetSection("OrderDbConnection").Value;

                    services.AddDbContext<ProductDbContext>(options =>
                    {
                        options.UseSqlServer(productDbConnectionString);
                    });

                    services.AddDbContext<OrderDbContext>(options =>
                    {
                        options.UseSqlServer(orderDbConnectionString);
                    });

                    services.Configure<AppConfig>(hostContext.Configuration.GetSection("AppConfig"));

                    services.AddMassTransit(cfg =>
                    {
                        cfg.AddConsumersFromNamespaceContaining<ProductConsumerAnchor>();
                        cfg.AddConsumersFromNamespaceContaining<OrderConsumerAnchor>();
                        cfg.AddConsumersFromNamespaceContaining<ConsumerAnchor>();

                        //cfg.AddSagaStateMachinesFromNamespaceContaining<StateMachineAnchor>();
                        cfg.AddBus(ConfigureBus);
                    });

                    //services.AddSingleton(typeof(ISagaRepository<>), typeof(InMemorySagaRepository<>));

                    services.AddSingleton<IHostedService, MassTransitConsoleHostedService>();

                    // Add resolver
                    DependencyContainer.RegisterServices(services);
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                });

            await builder.RunConsoleAsync().ConfigureAwait(false);
        }

        private static IBusControl ConfigureBus(IServiceProvider provider)
        {
            var options = provider.GetRequiredService<IOptions<AppConfig>>().Value;

            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(options.Host, options.VirtualHost, h =>
                {
                    h.Username(options.Username);
                    h.Password(options.Password);
                });


                cfg.ReceiveEndpoint(host, "process-order", e =>
                {
                    e.Consumer(() => new ProcessOrderConsumer(provider.GetRequiredService<IConfiguration>()));

                    var a = new ProcessOrderRequestProxy(provider.GetRequiredService<IConfiguration>());
                    var b = new ResponseProxy();
                    e.Instance(a);
                    e.Instance(b);

                });


                var compensateCreateOrderAddress = new Uri(string.Concat("rabbitmq://localhost/", "compensate_createorder"));
                cfg.ReceiveEndpoint(host, "execute_createorder", e =>
                {
                    e.ExecuteActivityHost<CreateOrderActivity, CreateOrderArguments>(compensateCreateOrderAddress, () => new
                        CreateOrderActivity(provider.GetRequiredService<IOrderRepository>(), provider.GetRequiredService<IOrderDetailRepository>()));

                });
                cfg.ReceiveEndpoint(host, "compensate_createorder", e =>
                {
                    e.CompensateActivityHost<CreateOrderActivity, CreateOrderLog>(() => new
                        CreateOrderActivity(provider.GetRequiredService<IOrderRepository>(), provider.GetRequiredService<IOrderDetailRepository>()));
                });

                var compensateReserveProductAddress = new Uri(string.Concat("rabbitmq://localhost/", "compensate_reserveproduct"));
                cfg.ReceiveEndpoint(host, "execute_reserveproduct", e =>
                {
                    e.ExecuteActivityHost<ReserveProductActivity, ReserveProductArguments>(compensateReserveProductAddress, () =>
                        new ReserveProductActivity(provider.GetRequiredService<IProductRepository>()));
                });
                cfg.ReceiveEndpoint(host, "compensate_reserveproduct", e =>
                {
                    e.CompensateActivityHost<ReserveProductActivity, ReserveProductLog>(() =>
                        new ReserveProductActivity(provider.GetRequiredService<IProductRepository>()));
                });

                var compensateApproveOrderAddress = new Uri(string.Concat("rabbitmq://localhost/", "compensate_approveorder"));
                cfg.ReceiveEndpoint(host, "execute_approveorder", e =>
                {
                    e.ExecuteActivityHost<ApproveOrderActivity, ApproveOrderArguments>(compensateApproveOrderAddress, () => new
                        ApproveOrderActivity(provider.GetRequiredService<IOrderRepository>()));

                });
                cfg.ReceiveEndpoint(host, "compensate_approveorder", e =>
                {
                    e.CompensateActivityHost<ApproveOrderActivity, ApproveOrderLog>(() => new
                        ApproveOrderActivity(provider.GetRequiredService<IOrderRepository>()));
                });

              
            });
        }
    }
}