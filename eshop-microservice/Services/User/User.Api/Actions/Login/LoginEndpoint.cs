using User.Api.Data.Dtos;

namespace User.Api.Actions.Login
{
    public record LoginRequest(LoginDto LoginDto);
    public record LoginResponse(UserDto UserDto);
    public class LoginEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/login", async (LoginRequest LoginDto, ISender sender) =>
            {
                var command = LoginDto.Adapt<LoginCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<LoginResponse>();

                return Results.Ok(response);
            });
        }
    }
}
