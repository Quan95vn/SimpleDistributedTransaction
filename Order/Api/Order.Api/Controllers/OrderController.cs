using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Order.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IBusControl _bus;

        public OrderController(IBusControl bus)
        {
            _bus = bus;
        }


        public async Task<IActionResult> Get()
        {
            await _bus.Publish<CreateProduct>(new
            {
                ProductId = NewId.NextGuid(),
                Name = "Product 2",
                Price = 50,
                QuantityInStock = 100,
                CreatedDate = DateTime.Now
            });

            return Content("");
        }
    }
}