using Shopping.Web.Models.Basket;

namespace Shopping.Web.Pages.Shared
{
    public class CartModel(IBasketService basketService, ILogger<CartModel> logger) : PageModel
    {

        public ShoppingCartModel ShoppingCartModel { get; set; } = new ShoppingCartModel();
        public async Task<IActionResult> OnGet()
        {
            var basket = await basketService.LoadUserBasket();
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(Guid productId)
        {
            logger.LogInformation("Remove To Cart Button Clicked!");
            var basket = await basketService.LoadUserBasket();
            var itemToRemove = basket.Items.FirstOrDefault(item => item.ProductId == productId);
            if (itemToRemove != null)
            {
                basket.Items.Remove(itemToRemove);
                await basketService.StoreBasket(new StoreBasketRequest(basket));
            }
            return RedirectToPage();
        }
    }
}
