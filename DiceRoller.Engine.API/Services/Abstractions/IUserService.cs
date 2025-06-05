using System.Security.Claims;
using DiceRoller.Domain.Entities;

namespace DiceRoller.Engine.API.Services.Abstractions
{
    public interface IUserService
    {
        Task<User?> GetUserAsync(ClaimsPrincipal claimsPrincipal);
    }
}
