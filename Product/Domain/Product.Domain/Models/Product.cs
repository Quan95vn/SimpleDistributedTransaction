using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Domain.Models
{
    public class Product
    {
        public Guid ProductId { get; private set; }

        public string Name { get; private set; }

        public decimal Price { get; private set; }

        public int QuantityInStock { get; private set; }

        public DateTime CreatedDate { get; private set; }

        public DateTime UpdatedDate { get; private set; }
    }
}
