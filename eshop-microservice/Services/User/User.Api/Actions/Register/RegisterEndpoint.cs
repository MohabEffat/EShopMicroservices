using User.Api.Data.Dtos;

namespace User.Api.Actions.Register
{
    public record RegisterRequest(RegisterDto RegisterDto);
    public record RegisterResponse(UserDto UserDto);
    public class RegisterEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/register", async (RegisterRequest RegisterDto, ISender sender) =>
            {
                var command = RegisterDto.Adapt<RegisterCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<RegisterResponse>();

                return Results.Created($"/users/{response.UserDto.Id}", response);
            });
        }
    }
}
