using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Data.Context
{
    public class ProductDbContext : DbContext
    {
      

        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        public DbSet<Domain.Models.Product> Products { get; set; }
    }
}
