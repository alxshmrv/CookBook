using CookBook.Abstractions;
using CookBook.Configuration.JWT;
using CookBook.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CookBook.Services
{
    public class JwtTokenGenerator(IOptions<JwtSettings> jwtSettings) :
        IJwtTokenGenerator
    {
        public string GenerateToken(User user)
        {
            var options = jwtSettings.Value;

            SigningCredentials signingCredentials = new(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Secret)),
            SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.Name),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var expiration = DateTime.UtcNow.AddMinutes(20);

            JwtSecurityToken securityToken = new(
                issuer: options.Issuer,
                audience: options.Audience,
                expires: expiration,
                claims: claims,
                signingCredentials: signingCredentials
                );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }
    }
}
