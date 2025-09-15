namespace Basket.Api.Model
{
    public class ShoppingCartItems
    {
        public Guid ProductId { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public string Color { get; set; } = default!;
        public string ProductName { get; set; } = default!;
    }
}
