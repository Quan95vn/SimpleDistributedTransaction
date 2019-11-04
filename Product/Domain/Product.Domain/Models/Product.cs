using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Domain.Models
{
    /// <summary>
    /// Class Product
    /// </summary>
    public class Product
    {
        public Product(Guid productId, string name, decimal price, int quantityInStock, DateTime createdDate)
        {
            ProductId = productId;
            Name = name;
            Price = price;
            QuantityInStock = quantityInStock;
            CreatedDate = createdDate;
        }

        public Guid ProductId { get; private set; }

        public string Name { get; private set; }

        public decimal Price { get; private set; }

        public int QuantityInStock { get; private set; }

        public DateTime CreatedDate { get; private set; }

        public DateTime UpdatedDate { get; private set; }
    }
}
