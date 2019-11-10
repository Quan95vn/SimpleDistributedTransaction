using Contracts;
using MassTransit;
using MassTransit.Courier;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Order.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Order.Domain.Consumers
{
    /// <summary>
    /// Class ProcessOrderConsumer
    /// </summary>
    public class ProcessOrderConsumer : IConsumer<ProcessOrder>
    {
        private readonly ILogger<ProcessOrderConsumer> _log;
        private readonly IConfiguration _configuration;
        private readonly IOrderRepository _orderRepository;


        public ProcessOrderConsumer(ILoggerFactory loggerFactory, IConfiguration configuration, IOrderRepository orderRepository)
        {
            _log = loggerFactory.CreateLogger<ProcessOrderConsumer>();
            _configuration = configuration;
            _orderRepository = orderRepository;
        }

        public async Task Consume(ConsumeContext<ProcessOrder> context)
        {
            try
            {
                if (!string.IsNullOrEmpty(context.Message.ErrorMessage))
                {
                    await context.RespondAsync<OrderSubmitted>(new
                    {
                        context.Message.OrderId,
                        context.Message.ErrorMessage
                    });

                    return;
                }

                RoutingSlipBuilder builder = new RoutingSlipBuilder(context.Message.OrderId);
                // get configs
                var settings = new Settings(_configuration);

                // Add activities
                builder.AddActivity(settings.CreateOrderActivityName, settings.CreateOrderExecuteAddress);
                builder.SetVariables(new { context.Message.OrderId, context.Message.Address, context.Message.CreatedDate, context.Message.OrderDetails });

                builder.AddActivity(settings.ReserveProductActivityName, settings.ReserveProductExecuteAddress);
                builder.SetVariables(new { context.Message.OrderDetails });

                builder.AddActivity(settings.ApproveOrderActivityName, settings.ApproveOrderExecuteAddress);
                builder.SetVariables(new { context.Message.OrderId });
                await context.Execute(builder.Build());
              
                await context.RespondAsync<OrderSubmitted>(new
                {
                    context.Message.OrderId
                });
              
            }
            catch (Exception ex)
            {
                _log.LogError("Can not create Order {OrderId}", context.Message.OrderId);
                throw new Exception(ex.Message);
            }
        }

        private class Settings
        {
            public Settings(IConfiguration configuration)
            {
                CreateOrderActivityName = configuration.GetSection("ActivityConfig:CreateOrderActivityName").Value;
                CreateOrderExecuteAddress = new Uri(configuration.GetSection("ActivityConfig:CreateOrderExecuteAddress").Value);
                ApproveOrderActivityName = configuration.GetSection("ActivityConfig:ApproveOrderActivityName").Value;
                ApproveOrderExecuteAddress = new Uri(configuration.GetSection("ActivityConfig:ApproveOrderExecuteAddress").Value);
                ReserveProductActivityName = configuration.GetSection("ActivityConfig:ReserveProductActivityName").Value;
                ReserveProductExecuteAddress = new Uri(configuration.GetSection("ActivityConfig:ReserveProductExecuteAddress").Value);
            }

            public string CreateOrderActivityName { get; }

            public Uri CreateOrderExecuteAddress { get; }

            public string ApproveOrderActivityName { get; }

            public Uri ApproveOrderExecuteAddress { get; }

            public string ReserveProductActivityName { get; }

            public Uri ReserveProductExecuteAddress { get; }
        }
    }
}