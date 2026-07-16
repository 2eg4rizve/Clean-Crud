using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CleanCrud.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CleanCrud.Api.Authentication;

public sealed class JwtTokenService(IOptions<JwtSettings> settings, UserManager<ApplicationUser> userManager)
{
    private readonly JwtSettings _settings = settings.Value;

    public async Task<AuthResponse> CreateAsync(ApplicationUser user)
    {
        var roles = await userManager.GetRolesAsync(user);
        var expires = DateTime.UtcNow.AddMinutes(_settings.ExpiryMinutes);
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName ?? string.Empty)
        };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
        var token = new JwtSecurityToken(
            _settings.Issuer,
            _settings.Audience,
            claims,
            expires: expires,
            signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

        return new AuthResponse(new JwtSecurityTokenHandler().WriteToken(token), expires, user.Email ?? string.Empty, roles.ToList());
    }
}
