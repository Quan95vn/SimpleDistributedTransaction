using Contracts;
using MassTransit;
using MediatR;
using Order.Domain.Events;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Domain.DomainHandler
{
    public class DomainNotificationHandler : IConsumer<SubmitOrder>, INotificationHandler<DomainNotification>
    {
        private List<string> _notifications;

        public DomainNotificationHandler()
        {
            _notifications = new List<string>();
        }

        public Task Consume(ConsumeContext<SubmitOrder> context)
        {
            _notifications.Add("ABCDEFGHJKL");
            return Task.CompletedTask;
        }

        public virtual List<string> GetNotifications()
        {
            return _notifications;
        }

        public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}