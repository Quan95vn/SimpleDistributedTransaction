using Contracts;
using MassTransit;
using MassTransit.Definition;
using MassTransit.Saga;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Order.Data.Context;
using Product.Data.Context;
using Product.Domain.Consumers;
using SimpleDistributedTransactio.Infra.IoC;
using System;
using System.IO;
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

        static string ConfigureConnectionString(IServiceProvider provider)
        {
            var options = provider.GetRequiredService<IOptions<ConnectionString>>().Value;
            return options.DbConnection;
        }

        static IBusControl ConfigureBus(IServiceProvider provider)
        {
            var options = provider.GetRequiredService<IOptions<AppConfig>>().Value;

            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(options.Host, options.VirtualHost, h =>
                {
                    h.Username(options.Username);
                    h.Password(options.Password);
                });

                cfg.ConfigureEndpoints(provider, new KebabCaseEndpointNameFormatter());
            });
        }
    }
}