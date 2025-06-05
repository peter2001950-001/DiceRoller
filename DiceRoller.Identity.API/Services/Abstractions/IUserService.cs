using DiceRoller.Identity.API.Models.Requests;
using DiceRoller.Identity.API.Models.Responses;

namespace DiceRoller.Identity.API.Services.Abstractions
{
    public interface IUserService
    {
        Task CreateUserAsync(RegisterRequest registerRequest);
        Task<TokenResponse> LoginUserAsync(LoginRequest request);
    }
}
