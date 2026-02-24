using Backend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtService
{
    private readonly IConfiguration _config;

    public JwtService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(User user)
    {
        var jwtSection = _config.GetSection("Jwt");

        var keyString = jwtSection["Key"]
            ?? throw new Exception("JWT Key missing");

        var issuer = jwtSection["Issuer"]
            ?? throw new Exception("JWT Issuer missing");

        var audience = jwtSection["Audience"]
            ?? throw new Exception("JWT Audience missing");

        var expireMinutesString = jwtSection["ExpireMinutes"]
            ?? throw new Exception("JWT ExpireMinutes missing");

        if (!double.TryParse(expireMinutesString, out var expireMinutes))
            throw new Exception("Invalid JWT ExpireMinutes value");

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(keyString)
        );

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var claims = new[]
        {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
        new Claim(ClaimTypes.Role, user.Role?.Name ?? "USER")
    };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expireMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}