namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.Order.Id);
            var order = await dbContext.Orders.FindAsync([orderId], cancellationToken: cancellationToken);

            if (order is null)
            {
                throw new OrderNotFoundException(command.Order.Id);
            }
            UpdateOrderWithNewValues(order, command.Order);

            dbContext.Orders.Update(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateOrderResult(true);
        }

        private void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
        {
            var UpdatedShippingAddress = Address.Of(
                orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName,
                orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine,
                orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State,
                orderDto.ShippingAddress.ZipCode);

            var UpdatedBillingAddress = Address.Of(
                orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName,
                orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine,
                orderDto.BillingAddress.Country, orderDto.BillingAddress.State,
                orderDto.BillingAddress.ZipCode);

            var UpdatedPaymentDetails = Payment.Of(
                orderDto.Payment.CardNumber, orderDto.Payment.CardHolderName,
                orderDto.Payment.Expiration, orderDto.Payment.Cvv,
                orderDto.Payment.PaymentMethod);

            order.Update(
                orderName: OrderName.Of(orderDto.OrderName),
                shippingAddress: UpdatedShippingAddress,
                billingAddress: UpdatedBillingAddress,
                payment: UpdatedPaymentDetails,
                status: orderDto.Status);
        }
    }
}
