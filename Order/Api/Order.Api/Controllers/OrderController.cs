using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Order.Api.ViewModels;
using Order.Domain.Consumers;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Post([FromBody] CreateOrderViewModel value, CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Invalid model state");

                var response = await _processOrderClient.GetResponse<OrderSubmitted>(new
                {
                    OrderId = NewId.NextGuid(),
                    value.Address,
                    value.CreatedDate,
                    value.OrderDetails,
                }, cancellationToken);

                // Base on ErrorMessage 
                if (!string.IsNullOrEmpty(response.Message.ErrorMessage))
                {
                    return BadRequest(new
                    {
                        isSuccess = false,
                        errors = response.Message.ErrorMessage
                    });
                }

                return Ok(new
                {
                    isSuccess = true,
                    response.Message.OrderId
                });
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