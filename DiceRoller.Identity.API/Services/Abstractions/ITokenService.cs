using DiceRoller.Domain.Entities;
using DiceRoller.Identity.API.Models.Responses;

namespace DiceRoller.Identity.API.Services.Abstractions
{
    public interface ITokenService
    {
        TokenResponse GenerateToken(User user);
    }
}
