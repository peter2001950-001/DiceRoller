using DiceRoller.Domain.Abstractions.Repositories;
using DiceRoller.Domain.Entities;
using DiceRoller.Infrastructure.Data;

namespace DiceRoller.Infrastructure.Repositories
{
    public class DiceRollRepository : CrudBaseRepository<DiceRoll>, IDiceRollRepository
    {
        public DiceRollRepository(DiceDbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<DiceRoll> Query => DbContext.DiceRolls;
    }
}
