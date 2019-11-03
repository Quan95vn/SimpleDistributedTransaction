using System;
using System.Collections.Generic;
using System.Text;

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
