using System.Text;
using DiceRoller.Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DiceRoller.Infrastructure.Utilities
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            var config = services.BuildServiceProvider().GetService<IConfiguration>()!;
            services.Configure<IdentityOptions>(config.GetSection("Identity"));

            var optionsSection = config.GetSection("Identity");
            var identityOptions = optionsSection.Get<IdentityOptions>();
;
            // Add JWT authentication
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Bearer";
                    options.DefaultChallengeScheme = "Bearer";
                })
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = identityOptions.Issuer,
                        ValidAudience = identityOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(identityOptions.SigningKey))
                    };
                });

            services.AddAuthorization();
            return services;
        }
    }
}
