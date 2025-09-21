namespace Ordering.Application.EventHandlers.DomainEvents
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
