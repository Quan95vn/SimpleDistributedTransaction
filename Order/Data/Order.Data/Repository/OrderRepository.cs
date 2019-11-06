using Order.Data.Context;
using Order.Domain.Interfaces;
using Order.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Order.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task Add(Domain.Models.Order order)
        {
            await _context.AddAsync(order);
            await _context.SaveChangesAsync();
        }
    }
}
