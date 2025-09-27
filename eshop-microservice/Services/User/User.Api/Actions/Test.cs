using Microsoft.AspNetCore.Authorization;

namespace User.Api.Actions
{
    public class Test : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/test", [Authorize] () => "User.Api is working!")
                .RequireAuthorization();
        }
    }
}
