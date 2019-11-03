using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Data.Context
{
    public class ProductContext : DbContext
    {
      

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        public DbSet<Domain.Models.Product> Products { get; set; }
    }
}
