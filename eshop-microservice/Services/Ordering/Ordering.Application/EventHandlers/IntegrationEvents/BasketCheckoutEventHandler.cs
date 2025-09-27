using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.EventHandlers.IntegrationEvents
{
    public class BasketCheckoutEventHandler
        (ISender sender, ILogger<BasketCheckoutEventHandler> logger)
        : IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            logger.LogInformation("BasketCheckoutEventHandler consumed {IntegrationEvent}", context.Message);

            var command = MapToOrderCommand(context.Message);

            await sender.Send(command, context.CancellationToken);
        }

        private CreateOrderCommand MapToOrderCommand(BasketCheckoutEvent message)
        {
            var orderId = Guid.NewGuid();

            var addressDto = new AddressDto(
                message.FirstName, message.LastName,
                message.EmailAddress, message.AddressLine,
                message.Country, message.State,
                message.ZipCode);

            var paymentDto = new PaymentDto(
                message.CardNumber, message.CardHolderName,
                message.Expiration, message.CVV,
                message.PaymentMethod);

            // Use the basket items from the message instead of hardcoded values
            var orderItems = message.BasketItems?.Select(item =>
                new OrderItemDto(orderId, item.ProductId, item.Quantity, item.Price))?.ToList()
                ?? new List<OrderItemDto>();

            // If no basket items were provided, log a warning but don't fail
            if (orderItems.Count == 0)
            {
                logger.LogWarning("No basket items found in checkout event for user {UserName}", message.UserName);
            }

            //var orderItems = new List<OrderItemDto>
            //{
            //    new(orderId, new Guid("9e4e47b1-8f7e-4d78-b6a3-18a51389d8e2"), 2, message.TotalPrice),
            //    new(orderId, new Guid("3f6f8d46-b9a4-4bbd-bc91-403bcf00e7cd"), 3, message.TotalPrice)
            //};

            var orderDto = new OrderDto(
                orderId,
                message.CustomerId,
                message.UserName,
                addressDto,
                addressDto,
                paymentDto,
                OrderStatus.Pending,
                orderItems,
                message.TotalPrice
            );

            return new CreateOrderCommand(orderDto);
        }
    }
}
