using DiceRoller.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiceRoller.Infrastructure.Data
{
    public class DiceDbContext : DbContext
    {
        public DiceDbContext(DbContextOptions<DiceDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = default!;

        public DbSet<DiceRoll> DiceRolls { get; set; } = default!;
    }
}
