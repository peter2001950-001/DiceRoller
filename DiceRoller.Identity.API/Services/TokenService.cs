using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DiceRoller.Domain.Entities;
using DiceRoller.Identity.API.Models.Responses;
using DiceRoller.Identity.API.Services.Abstractions;
using DiceRoller.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DiceRoller.Identity.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly IdentityOptions _identityOptions;


        public TokenService(IOptions<IdentityOptions> identityOptions)
        {
            _identityOptions = identityOptions.Value;
        }
        public TokenResponse GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.FirstName + user.LastName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_identityOptions.SigningKey));
            var credsToken = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _identityOptions.Issuer,
                audience: _identityOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(_identityOptions.ExpirationSeconds),
                signingCredentials: credsToken);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new TokenResponse(tokenString);
        }
    }
}
