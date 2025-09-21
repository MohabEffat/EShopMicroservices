using MassTransit;
using Microsoft.FeatureManagement;
namespace Ordering.Application.EventHandlers.DomainEvents
{
    public class OrderCreatedEventHandler(IFeatureManager featureManager,
        IPublishEndpoint publishEndpoint, ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Domain Event Handled: {domainEvent.GetType().Name}");

            if (await featureManager.IsEnabledAsync("OrderFulfilment"))
            {
                var orderCreatedIntegrationEvent = domainEvent.order.ToOrderDto();
                await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
            }

        }
    }
}
