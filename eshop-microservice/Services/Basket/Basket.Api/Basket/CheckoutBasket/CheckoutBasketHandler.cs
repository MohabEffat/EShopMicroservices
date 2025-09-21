using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.Api.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(CheckoutBasketDto CheckoutBasket) : ICommand<CheckoutBasketResult>;
    public record CheckoutBasketResult(bool IsSuccess);

    public sealed class CheckoutBasketValidator : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketValidator()
        {
            RuleFor(x => x.CheckoutBasket).NotNull().WithMessage("CheckoutBasket is required.");
            RuleFor(x => x.CheckoutBasket.UserName).NotEmpty().WithMessage("UserName is required.");
        }
    }

    public class CheckoutBasketHandler
        (IBasketRepository basketRepository, IPublishEndpoint publishEndpoint)
        : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetBasket(command.CheckoutBasket.UserName);

            if (basket == null || !basket.Items.Any())
            {
                 return new CheckoutBasketResult(false);
            }
        
            var eventMessage = command.CheckoutBasket.Adapt<BasketCheckoutEvent>();
            eventMessage.TotalPrice = basket.TotalPrice;
            
            // Add basket items to the event
            // eventMessage.BasketItems = basket.Items.Select(item => new BasketItemEvent
            // {
            //     ProductId = item.ProductId,
            //     ProductName = item.ProductName,
            //     Quantity = item.Quantity,   
            //     Price = item.Price
            // }).ToList();

            await publishEndpoint.Publish(eventMessage, cancellationToken);

            await basketRepository.DeleteBasket(basket.UserName, cancellationToken);

            return new CheckoutBasketResult(true);
        }
    }
}
