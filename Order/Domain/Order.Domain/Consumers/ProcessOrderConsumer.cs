﻿using Contracts;
using Masstransit.Activities;
using MassTransit;
using MassTransit.Courier;
using MassTransit.Courier.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Order.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public ProcessOrderConsumer(
            ILoggerFactory loggerFactory, 
            IConfiguration configuration,
            IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository)
        {
            _log = loggerFactory.CreateLogger<ProcessOrderConsumer>();
            _configuration = configuration;
            _orderRepository = orderRepository;
        }

        public async Task Consume(ConsumeContext<ProcessOrder> context)
        {
            try
            {
                RoutingSlipBuilder builder = new RoutingSlipBuilder(context.Message.OrderId);

               

                //var orderDetails = context.Message.OrderDetails;
                //foreach(var orderDetail in orderDetails)
                //{
                //    var model = new Models.OrderDetail(
                //        NewId.NextGuid(), 
                //        context.Message.OrderId, 
                //        orderDetail.ProductId, 
                //        orderDetail.Quantity
                //    );
                //    await _orderDetailRepository.Add(model);
                //}

                var orderId = NewId.NextGuid();

                await context.RespondAsync<OrderSubmitted>(new
                {
                    orderId
                });

                // get configs
                var settings = new Settings(_configuration);
                // Add activities

                //builder.AddActivity(settings.CreateOrderActivityName, settings.CreateOrderExecuteAddress);
                //builder.SetVariables(new { context.Message.OrderId, context.Message.Address, context.Message.CreatedDate, context.Message.OrderDetails });

                builder.AddActivity(settings.ReserveProductActivityName, settings.ReserveProductExecuteAddress);
                builder.SetVariables(new { context.Message.OrderDetails });

                //builder.AddActivity(settings.ApproveOrderActivityName, settings.ApproveOrderExecuteAddress);


                await context.Execute(builder.Build());
            }
            catch (Exception ex)
            {
                _log.LogError("Can not create Order {OrderId}", context.Message.OrderId);
                throw new ActivityExecutionException(ex.Message);
            }
        }

        class Settings
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
