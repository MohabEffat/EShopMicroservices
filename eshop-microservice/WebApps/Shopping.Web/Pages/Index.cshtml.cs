using Shopping.Web.Models.Basket;

namespace Shopping.Web.Pages
{
    public class IndexModel(IBasketService basketService, ICatalogService catalogService, ILogger<IndexModel> _logger ) : PageModel
    {
        public IEnumerable<ProductModel> ProductList { get; set; } = new List<ProductModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            _logger.LogInformation("Getting products for the index page.");
            var response = await catalogService.GetProducts();
            ProductList = response.Products;
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {
            _logger.LogInformation("Add To Cart Button Clicked!");

            var productResponse = await catalogService.GetProduct(productId);

            var basket = await basketService.LoadUserBasket();

            basket.Items.Add(new ShoppingCartItemModel
            {
                ProductId = productId,
                ProductName = productResponse.Product.Name,
                Price = productResponse.Product.Price,
                Quantity = 1,
                Color = "Black"
            });

            await basketService.StoreBasket(new StoreBasketRequest(basket));

            // Redirect to Cart page
            return RedirectToPage("Cart");  
        }
    }
}
