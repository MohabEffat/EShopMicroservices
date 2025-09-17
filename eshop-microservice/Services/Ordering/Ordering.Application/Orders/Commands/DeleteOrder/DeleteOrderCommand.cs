namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public record DeleteOrderCommand(Guid orderId) : ICommand<DeleteOrderResult>;
    public record DeleteOrderResult(bool IsSuccess);
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(x => x.orderId).NotEmpty().WithMessage("OrderId is Required!");
        }
    }
}
