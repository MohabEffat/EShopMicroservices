namespace Catalog.Api.Products.DeleteProduct
{
    public record DeleteProductRequest(Guid Id) : IRequest<DeleteProductResponse>;
    public record DeleteProductResponse(bool IsDeleted);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id:guid}", async (Guid id, ISender sender) =>
            {
                var command = new DeleteProductCommand(id);

                var result = await sender.Send(command);

                var response = result.Adapt<DeleteProductResponse>();

                return Results.Ok(response);
            })
            .WithDescription("Delete a product by its ID")
            .WithName("DeleteProduct")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Deletes a product from the catalog by its unique identifier.");
        }
    }
}
