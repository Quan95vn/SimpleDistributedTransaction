using Microsoft.EntityFrameworkCore;
using Order.Data.Context;
using Order.Domain.Interfaces;
using System;
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

        public async Task<Domain.Models.Order> GetByOrderId(Guid orderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId);
        }

        public async Task Add(Domain.Models.Order order)
        {
            _context.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Domain.Models.Order order)
        {
            _context.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}