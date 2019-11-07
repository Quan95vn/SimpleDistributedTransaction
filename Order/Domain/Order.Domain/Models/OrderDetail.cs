using System;

namespace Order.Domain.Models
{
    public class OrderDetail
    {
        public OrderDetail(Guid orderDetailId, Guid orderId, Guid productId, int quantity)
        {
            OrderDetailId = orderDetailId;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }


        public Guid OrderDetailId { get; private set; }

        public Guid OrderId { get; private set; }

        public Guid ProductId { get; private set; }

        public int Quantity { get; private set; }
    }
}