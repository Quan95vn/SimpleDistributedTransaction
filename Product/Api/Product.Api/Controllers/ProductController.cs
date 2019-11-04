using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<HttpResponseMessage> Post([FromBody] CreateProductViewModel value)
        {
            var respond = await _createProductClient.GetResponse<ProductCreated>(new
            {
                ProductId = value.ProductId,
                Name = value.Name,
                Price = value.Price,
                CreatedDate = value.CreatedDate,
            });

            return null;
        }
    }
}