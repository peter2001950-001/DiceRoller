using System.Security.Claims;
using DiceRoller.Domain.Abstractions.Repositories;
using DiceRoller.Domain.Entities;
using DiceRoller.Engine.API.Services.Abstractions;

namespace DiceRoller.Engine.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            var emailClaim = claimsPrincipal.FindFirst(ClaimTypes.Email);
            if (emailClaim == null) return null;

            var user = await _userRepository.GetFirstOrDefaultAsync(x => x.Email == emailClaim.Value);
            return user;
        }
    }
}
