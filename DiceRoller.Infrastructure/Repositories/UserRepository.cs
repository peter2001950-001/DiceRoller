using DiceRoller.Domain.Abstractions.Repositories;
using DiceRoller.Domain.Entities;
using DiceRoller.Infrastructure.Data;

namespace DiceRoller.Infrastructure.Repositories
{
    public class UserRepository : CrudBaseRepository<User>, IUserRepository
    {
        public UserRepository(DiceDbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<User> Query => DbContext.Users;
    }
}
