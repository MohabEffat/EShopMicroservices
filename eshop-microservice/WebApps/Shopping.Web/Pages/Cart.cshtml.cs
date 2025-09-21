using Shopping.Web.Models.Basket;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Web.Services;

namespace Shopping.Web.Pages.Shared
{
    public class CartModel(IBasketService basketService, ICatalogService catalogService, ILogger<CartModel> logger) : PageModel
    {

        public ShoppingCartModel ShoppingCartModel { get; set; } = new ShoppingCartModel();
        public async Task<IActionResult> OnGet()
        {
            var basket = await basketService.LoadUserBasket();
            ShoppingCartModel = basket;
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {
            logger.LogInformation("Add To Cart Button Clicked for Product ID: {ProductId}", productId);
            
            // Get product details from catalog service
            var productResponse = await catalogService.GetProduct(productId);
            if (productResponse?.Product == null)
            {
                logger.LogWarning("Product not found: {ProductId}", productId);
                return RedirectToPage();
            }
            
            var basket = await basketService.LoadUserBasket();
            
            // Check if the product is already in the basket
            var existingItem = basket.Items.FirstOrDefault(item => item.ProductId == productId);
            if (existingItem != null)
            {
                // Increment quantity if product already exists
                existingItem.Quantity++;
            }
            else
            {
                // Add new item to basket with product details
                basket.Items.Add(new ShoppingCartItemModel
                {
                    ProductId = productId,
                    ProductName = productResponse.Product.Name,
                    Price = productResponse.Product.Price,
                    Quantity = 1,
                    Color = "Default"
                });
            }
            
            // Save the updated basket
            await basketService.StoreBasket(new StoreBasketRequest(basket));
            return RedirectToPage();
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
