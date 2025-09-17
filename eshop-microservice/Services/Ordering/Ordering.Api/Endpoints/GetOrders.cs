namespace Ordering.Api.Endpoints
{
    //public record GetOrdersRequest(PaginationRequest PaginationRequest);
    public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);

    public class GetOrders : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var query = new GetOrdersQuery(request);
                var result = await sender.Send(query);
                var response = result.Adapt<GetOrdersResponse>();
                return Results.Ok(response);
            })
            .WithName("GetOrders")
            .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Gets a paginated list of orders")
            .WithDescription("Gets a paginated list of all orders.");
        }
    }
}
