using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiceRoller.Domain.Entities;

namespace DiceRoller.Domain.Abstractions.Repositories
{
    public interface IUserRepository : ICrudBaseRepository<User>
    {
    }
}
