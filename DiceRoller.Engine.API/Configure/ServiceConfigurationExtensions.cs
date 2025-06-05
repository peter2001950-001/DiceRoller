
using DiceRoller.Engine.API.Services;
using DiceRoller.Engine.API.Services.Abstractions;

namespace DiceRoller.Engine.API.Configure
{
    public static class ServiceConfigurationExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IDiceRollService, DiceRollService>();
            return serviceCollection;
        }
    }
}
