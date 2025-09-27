using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace User.Api.Services.TokenService
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _options;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<ApplicationUser> _userManager;

        public JwtService(IOptions<JwtOptions> options, UserManager<ApplicationUser> userManager)
        {
            _options = options.Value;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            _userManager = userManager;
        }
        public async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            var userClaims = new List<Claim>
            {
                // JWT standard claims
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), 
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.UserName!),
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(_options.ExpiryMinutes),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
