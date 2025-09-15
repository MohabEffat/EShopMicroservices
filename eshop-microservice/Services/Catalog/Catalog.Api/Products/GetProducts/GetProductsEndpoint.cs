namespace Catalog.Api.Products.GetProducts
{

    public record GetProductsRequest(int? pageNumber = 1, int? PageSize = 10);
    public record GetProductsResponse(IEnumerable<Product> Products); 
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
            {
                var query = request.Adapt<GetProductsQuery>();
                var result = await sender.Send(query);
                var response = new GetProductsResponse(result.Products);
                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Gets all products")
            .WithDescription("Retrieves a list of all products in the catalog.");
        }
    }
}
