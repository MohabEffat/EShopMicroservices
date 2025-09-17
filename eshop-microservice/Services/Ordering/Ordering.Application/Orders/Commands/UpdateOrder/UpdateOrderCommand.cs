namespace Ordering.Application.Orders.Commands
{
    public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>;
    public record UpdateOrderResult(bool IsSuccess);
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is Required!");
            RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("CustomerId is Required!");
            RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("Order must have at least one item!");
        }
    }

}
