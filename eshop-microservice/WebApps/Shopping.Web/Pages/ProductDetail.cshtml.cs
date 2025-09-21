using Shopping.Web.Models.Basket;

namespace Shopping.Web.Pages
{
    public class ProductDetailModel(IBasketService basketService, ICatalogService catalogService, ILogger<ProductDetailModel> logger) : PageModel
    {
        public ProductModel Product { get; set; } = new ProductModel();

        [BindProperty]
        public string Color { get; set; } = string.Empty;

        [BindProperty]
        public int Quantity { get; set; } = 1;

        public async Task<IActionResult> OnGetAsync(Guid productId)
        {
            if (productId == Guid.Empty)
            {
                logger.LogWarning("ProductId is empty on GET.");
                return BadRequest("Invalid product ID.");
            }

            var response = await catalogService.GetProduct(productId);
            if (response?.Product == null)
            {
                logger.LogWarning("Product not found for ID {ProductId}", productId);
                return NotFound();
            }

            Product = response.Product;
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {
            logger.LogInformation("Add to cart button clicked for ProductId {ProductId}", productId);

            if (productId == Guid.Empty)
            {
                logger.LogWarning("ProductId is empty on POST.");
                return BadRequest("Invalid product ID.");
            }

            if (Quantity <= 0)
            {
                logger.LogWarning("Invalid quantity: {Quantity}", Quantity);
                ModelState.AddModelError(nameof(Quantity), "Quantity must be at least 1.");
                return Page();
            }

            var productResponse = await catalogService.GetProduct(productId);
            if (productResponse?.Product == null)
            {
                logger.LogWarning("Product not found for ID {ProductId}", productId);
                return NotFound();
            }

            var basket = await basketService.LoadUserBasket();
            if (basket == null)
            {
                logger.LogError("Basket could not be loaded for the user.");
                return StatusCode(500, "Basket could not be loaded.");
            }

            basket.Items.Add(new ShoppingCartItemModel
            {
                ProductId = productId,
                ProductName = productResponse.Product.Name,
                Price = productResponse.Product.Price,
                Quantity = Quantity,
                Color = Color
            });

            await basketService.StoreBasket(new StoreBasketRequest(basket));

            // Redirect to Cart page
            return RedirectToPage("Cart");
        }
    }
}
