namespace Catalog.Api.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
         {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync(cancellation))
                return;

            session.Store<Product>(GetPreConfiguredProducts());
            await session.SaveChangesAsync(cancellation);

        }
        private static IEnumerable<Product> GetPreConfiguredProducts() => new List<Product>
    {
        new Product
        {
            Id = new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"),
            Name = "IPhone X",
            Description = "This phone is the company's biggest change to iPhone in years",
            ImageUrl = "product-1.png",
            Price = 950.00M,
            Categories = new List<string> { "Smart Phone" }
        },
        new Product
        {
            Id = new Guid("f87f4d6e-65c9-4a8a-9d55-4c54e0a6c101"),
            Name = "Samsung Galaxy S20",
            Description = "Flagship Samsung smartphone with amazing display",
            ImageUrl = "product-2.png",
            Price = 850.00M,
            Categories = new List<string> { "Smart Phone" }
        },
        new Product
        {
            Id = new Guid("a1b3f9b2-6f42-4d18-97a1-6cfbbadf3c12"),
            Name = "Google Pixel 6",
            Description = "Google’s newest Pixel with Tensor chip",
            ImageUrl = "product-3.png",
            Price = 799.00M,
            Categories = new List<string> { "Smart Phone" }
        },
        new Product
        {
            Id = new Guid("d2c60b8a-032b-4a0a-bda7-3b2fc5a89a2b"),
            Name = "Huawei P50",
            Description = "Powerful phone with Leica camera system",
            ImageUrl = "product-4.png",
            Price = 720.00M,
            Categories = new List<string> { "Smart Phone" }
        },
        new Product
        {
            Id = new Guid("4e2f9f27-3f6e-4b08-9dc1-8e13e55c5a7e"),
            Name = "Sony WH-1000XM4",
            Description = "Noise-cancelling over-ear headphones",
            ImageUrl = "product-5.png",
            Price = 350.00M,
            Categories = new List<string> { "Audio" }
        },
        new Product
        {
            Id = new Guid("8a9f0c2b-d7f1-4ac1-94de-9c14af6e1f20"),
            Name = "Apple MacBook Pro",
            Description = "Apple’s M1-powered MacBook Pro laptop",
            ImageUrl = "product-6.png",
            Price = 1999.00M,
            Categories = new List<string> { "Laptop" }
        },
        new Product
        {
            Id = new Guid("c34c2a8e-5f7c-4b28-8d73-fd4f56d2c67f"),
            Name = "Dell XPS 13",
            Description = "Compact premium ultrabook with great battery life",
            ImageUrl = "product-7.png",
            Price = 1400.00M,
            Categories = new List<string> { "Laptop" }
        },
        new Product
        {
            Id = new Guid("7e9d5f8c-23df-47ab-b39f-1f8a4b3e19af"),
            Name = "Logitech MX Master 3",
            Description = "Ergonomic wireless mouse for productivity",
            ImageUrl = "product-8.png",
            Price = 120.00M,
            Categories = new List<string> { "Accessories" }
        },
        new Product
        {
            Id = new Guid("1b5f2f0d-ded3-4f6e-97f3-2c1e9d45b23d"),
            Name = "Apple Watch Series 7",
            Description = "Smartwatch with fitness tracking and notifications",
            ImageUrl = "product-9.png",
            Price = 399.00M,
            Categories = new List<string> { "Wearables" }
        },
        new Product
        {
            Id = new Guid("93d2a6f8-45cb-44c9-b3f7-7ab5c0e2f1a6"),
            Name = "Amazon Echo Dot",
            Description = "Smart speaker with Alexa voice assistant",
            ImageUrl = "product-10.png",
            Price = 49.99M,
            Categories = new List<string> { "Smart Home" }
        }
    };
    }

}
