using Order.Data.Context;
using Order.Domain.Interfaces;
using Order.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Order.Data.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly OrderDbContext _context;

        public OrderDetailRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task Add(OrderDetail orderDetail)
        {
            await _context.AddAsync(orderDetail);
            await _context.SaveChangesAsync();
        }
    }
}
