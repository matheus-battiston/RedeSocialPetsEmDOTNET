using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Instapet.Domain.Contracts.Responses.Login;
using Microsoft.IdentityModel.Tokens;

namespace InstaPet.WebApi.Security;

public class TokenService
{
    public string GenerateToken(LoginResponse login)
    {
        var secretKey = Encoding.UTF8.GetBytes(TokenSettings.SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(TokenSettings.ExpiresInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey),
                SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", login.Id.ToString()),
                new Claim(ClaimTypes.Email, login.Email),
            })
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var stringToken = tokenHandler.WriteToken(token);

        return stringToken;
    }
}