using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Events;

namespace Ordering.Application.EventHandlers
{
    public class OrderUpdatedEventHandler(ILogger<OrderUpdatedEvent> logger) : INotificationHandler<OrderUpdatedEvent>
    {
        public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Domain Event Handled: {notification.GetType().Name}");
            return Task.CompletedTask;
        }
    }
}
