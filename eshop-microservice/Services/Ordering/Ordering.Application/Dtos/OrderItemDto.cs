namespace Ordering.Application.Dtos
{
    public record OrderItemDto(Guid orderId, Guid ProductId, int Quantity, decimal Price);

}
