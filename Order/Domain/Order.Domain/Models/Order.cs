using System;

namespace Order.Domain.Models
{
    public class Order
    {
        public Order(Guid orderId, string address, DateTime createdDate)
        {
            OrderId = orderId;
            Address = address;
            CreatedDate = createdDate;
        }

        public Guid OrderId { get; private set; }

        public string Address { get; private set; }

        public DateTime CreatedDate { get; private set; }

        public DateTime? UpdatedDate { get; private set; }
    }
}