using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Order.Api.ViewModels;

namespace Order.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IBusControl _bus;
        private readonly IRequestClient<ProcessOrder> _processOrderClient;
        public OrderController(IBusControl bus, IRequestClient<ProcessOrder> processOrderClient)
        {
            _bus = bus;
            _processOrderClient = processOrderClient;
        }


        public async Task<IActionResult> Get()
        {
            //await _bus.Publish<CreateProduct>(new
            //{
            //    ProductId = NewId.NextGuid(),
            //    Name = "Product 2",
            //    Price = 50,
            //    QuantityInStock = 100,
            //    CreatedDate = DateTime.Now
            //});

            return Ok();
        }

        [HttpPost("post")]
        public async Task<IActionResult> Post([FromBody] CreateOrderViewModel value)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Invalid model state");

                var (accepted, rejected) = await _processOrderClient.GetResponse<OrderSubmitted, OrderRejected>(new
                {
                    OrderId = NewId.NextGuid(),
                    value.Address,
                    value.CreatedDate,
                    value.OrderDetails,
                });

                if (accepted.IsCompleted)
                {
                    if (accepted.IsFaulted)
                        throw new Exception(accepted.Exception.Message);

                    return Ok();
                }

                await rejected;
                    throw new Exception(rejected.Exception.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    isSuccess = false,
                    errors = ex.Message
                });
            }
        }
    }
}