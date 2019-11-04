using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Product.Api.ViewModels;

namespace Product.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRequestClient<CreateProduct> _createProductClient;

        public ProductController(IRequestClient<CreateProduct> createProductClient)
        {
            _createProductClient = createProductClient;
        }

        [HttpGet("{id}")]
        public ActionResult<ProductViewModel> Get(Guid id)
        {
            return new ProductViewModel();
        }

        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductViewModel value)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Invalid model state");

                var response = await _createProductClient.GetResponse<ProductCreated>(new
                {
                    ProductId = NewId.NextGuid(),
                    value.Name,
                    value.Price,
                    value.QuantityInStock,
                    value.CreatedDate
                });

                var obj = new
                {
                    isSuccess = true,
                    data = response.Message
                };

                return Content(JsonConvert.SerializeObject(obj));
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