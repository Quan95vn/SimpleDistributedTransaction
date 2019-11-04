using System;

namespace Order.Domain.Models
{
    public class OrderDetail
    {
        public Guid OrderDetailId { get; private set; }

        public Guid OrderId { get; private set; }

        public Guid ProductId { get; private set; }

        public int Quantity { get; private set; }
    }
}