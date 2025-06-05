using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DiceRoller.Infrastructure.Data
{
    public class DiceDbContextFactory : IDesignTimeDbContextFactory<DiceDbContext>
    {
        public DiceDbContext CreateDbContext(string[] args)
        {
            string? connectionString;
            if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            {
                connectionString = args[0];
                Console.WriteLine("[Factory] Using connection string from args.");
            }
            else
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
              
                connectionString = configuration.GetConnectionString("DefaultConnection");
            }

            var optionsBuilder = new DbContextOptionsBuilder<DiceDbContext>();
            optionsBuilder.UseNpgsql(connectionString); // Or Npgsql, Sqlite, etc.

            return new DiceDbContext(optionsBuilder.Options);
        }
    }
}
