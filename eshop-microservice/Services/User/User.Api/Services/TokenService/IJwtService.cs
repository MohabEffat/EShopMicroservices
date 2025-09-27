namespace User.Api.Services.TokenService
{
    public interface IJwtService 
    {
        Task<string> GenerateTokenAsync(ApplicationUser applicationUser);
    }
}
