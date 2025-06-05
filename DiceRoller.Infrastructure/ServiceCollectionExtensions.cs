using Microsoft.Extensions.DependencyInjection;
using DiceRoller.Domain.Abstractions.Repositories;
using DiceRoller.Infrastructure.Data;
using DiceRoller.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DiceRoller.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            var config = services.BuildServiceProvider().GetService<IConfiguration>()!;

            services.AddDbContext<DiceDbContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
            });



            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDiceRollRepository, DiceRollRepository>();

            return services;
        }

        public static void RunDbMigrations(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            using var scope = provider.GetService<IServiceScopeFactory>().CreateScope();

            var applicationDbContext = scope.ServiceProvider.GetRequiredService<DiceDbContext>();
            applicationDbContext.Database.Migrate();
        }
    }
}
