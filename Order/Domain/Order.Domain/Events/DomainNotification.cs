using MediatR;

namespace Order.Domain.Events
{
    public class DomainNotification : INotification
    {
        public string Message { get; set; }
    }
}