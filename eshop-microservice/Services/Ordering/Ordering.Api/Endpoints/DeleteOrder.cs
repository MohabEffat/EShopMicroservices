namespace Ordering.Api.Endpoints
{
    //public record DeleteOrderRequest(Guid OrderId);
    public record DeleteOrderResponse(bool IsSuccess);
    public class DeleteOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/orders/{orderId:guid}", async (Guid orderId, ISender sender) =>
            {
                var command = new DeleteOrderCommand(orderId);
                var result = await sender.Send(command);
                var response = result.Adapt<DeleteOrderResponse>();
                return Results.Ok(response);
            })
            .WithName("DeleteOrder")
            .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Deletes an existing order")
            .WithDescription("Deletes an existing order for a customer.");
        }
    }
}
