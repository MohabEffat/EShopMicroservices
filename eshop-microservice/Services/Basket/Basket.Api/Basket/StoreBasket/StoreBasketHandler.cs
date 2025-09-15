using Discount.Grpc;

namespace Basket.Api.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult (string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.ShoppingCart).NotEmpty().WithMessage("ShoppingCart Can't Be Empty!");
            RuleFor(x => x.ShoppingCart.UserName).NotEmpty().WithMessage("UserName Is Required!");
        }
    }
    public class StoreBasketHandler
        (IBasketRepository basketRepository,DiscountProtoService.DiscountProtoServiceClient discountProto)
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            ShoppingCart cart = command.ShoppingCart;

            await GetDiscount(cart, cancellationToken);

            await basketRepository.StoreBasket(cart);

            return new StoreBasketResult(cart.UserName);
        }
        private async Task<ShoppingCart> GetDiscount(ShoppingCart cart, CancellationToken cancellationToken)
        {
            foreach (var item in cart.Items)
            {
                var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
                item.Price -= coupon.Amount;
            }
            return cart;
        }
    }
}
