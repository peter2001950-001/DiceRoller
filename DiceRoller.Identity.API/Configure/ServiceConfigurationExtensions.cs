using DiceRoller.Domain.Entities;
using DiceRoller.Identity.API.Models.Requests;
using DiceRoller.Identity.API.Services;
using DiceRoller.Identity.API.Services.Abstractions;
using DiceRoller.Identity.API.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace DiceRoller.Identity.API.Configure
{
    public static class ServiceConfigurationExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ITokenService, TokenService>();
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IPasswordHelper, PasswordHelper>();
            serviceCollection.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            serviceCollection.AddScoped<IValidator<RegisterRequest>, RegisterRequestValidator>();
            return serviceCollection;
        }
    }
}
