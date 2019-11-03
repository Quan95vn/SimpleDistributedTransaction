using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Data.Context
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Domain.Models.Order> Orders { get; set; }

        public DbSet<Domain.Models.OrderDetail> OrderDetails { get; set; }

    }
}
