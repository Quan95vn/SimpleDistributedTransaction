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
            Status = (byte)OrderStatus.Created;
        }

        public Guid OrderId { get; private set; }

        public string Address { get; private set; }

        public DateTime CreatedDate { get; private set; }

        public DateTime? UpdatedDate { get; private set; }

        public byte Status { get; private set; }

        public void SetApporveOrder()
        {
            Status = (byte)OrderStatus.Approved;
        }

        public void SetCanceledOrder()
        {
            Status = (byte)OrderStatus.Canceled;
        }

        public enum OrderStatus : byte
        {
            Created = 0,
            Approved = 1,
            Deliver = 2,
            Completed = 3,
            Canceled = 4
        }
    }
}