﻿using Contracts;
using Contracts.Models;
using MassTransit;
using MassTransit.Courier;
using MassTransit.Courier.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Domain.Proxies
{
    public class ProcessOrderRequestProxy : RoutingSlipRequestProxy<ProcessOrder>
    {
        private readonly IConfiguration _configuration;


        public ProcessOrderRequestProxy(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void BuildRoutingSlip(RoutingSlipBuilder builder, ConsumeContext<ProcessOrder> request)
        {
            // get configs
            var settings = new Settings(_configuration);

            // Add activities
            builder.AddActivity(settings.CreateOrderActivityName, settings.CreateOrderExecuteAddress);
            builder.SetVariables(new { request.Message.OrderId, request.Message.Address, request.Message.CreatedDate, request.Message.OrderDetails });

            //builder.AddActivity(settings.ReserveProductActivityName, settings.ReserveProductExecuteAddress);
            //builder.SetVariables(new { request.Message.OrderDetails });

            //request.Message.ErrorMessage = request.Message.ErrorMessage;
        }
    }

    public class ResponseProxy : RoutingSlipResponseProxy<ProcessOrder, OrderSubmitted>, IConsumer<SubmitOrder>
    {
        private string ErrorMessage { get; set; }

        protected override OrderSubmitted CreateResponseMessage(ConsumeContext<RoutingSlipCompleted> context, ProcessOrder request)
        {
            var a = context.Headers;

            return new Submitted(request.OrderId, ErrorMessage);
        }

        public Task Consume(ConsumeContext<SubmitOrder> context)
        {
            ErrorMessage = context.Message.Address;
            return Task.CompletedTask;
        }
    }

    class Submitted : OrderSubmitted
    {
        public Submitted(Guid orderId, string errorMessage)
        {
            OrderId = orderId;
            ErrorMessage = errorMessage;
        }

        public Guid OrderId { get; set; }

        public string ErrorMessage { get; set; }

        public Guid CorrelationId { get; set; }
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