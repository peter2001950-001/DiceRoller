using DiceRoller.Domain.Entities;

namespace DiceRoller.Identity.API.Services.Abstractions
{
    public interface IPasswordHelper
    {
        string GeneratePassword(User user, string password);
        bool VerifyPassword(User user, string hashedPassword, string password);
    }
}
