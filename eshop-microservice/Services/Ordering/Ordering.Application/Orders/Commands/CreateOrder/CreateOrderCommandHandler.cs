namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = CreateNewOrder(command.Order);
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new CreateOrderResult(order.Id.Value);
        }

        private Order CreateNewOrder(OrderDto orderDto)
        {
            var shippingAddress = Address.Of(
                orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName,
                orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine,
                orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State,
                orderDto.ShippingAddress.ZipCode);

             var billingAddress = Address.Of(
                orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName,
                orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine,
                orderDto.BillingAddress.Country, orderDto.BillingAddress.State,
                orderDto.BillingAddress.ZipCode);
            
            var paymentDetails = Payment.Of(
                orderDto.Payment.CardNumber, orderDto.Payment.CardHolderName,
                orderDto.Payment.Expiration, orderDto.Payment.Cvv,
                orderDto.Payment.PaymentMethod);

            var newOrder = Order.Create(id: OrderId.Of(Guid.NewGuid()), 
                customerId: CustomerId.Of(orderDto.CustomerId), 
                orderName: OrderName.Of(orderDto.OrderName), 
                shippingAddress: shippingAddress, 
                billingAddress: billingAddress, 
                payment: paymentDetails);

            foreach (var orderItemDto in orderDto.OrderItems)
            {
                newOrder.Add(ProductId.Of(orderItemDto.ProductId), orderItemDto.Quantity, orderItemDto.Price);
            }

            return newOrder;
        }
    }
}
