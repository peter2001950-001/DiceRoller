using DiceRoller.Domain.Abstractions.Pagination;
using DiceRoller.Domain.Entities;
using DiceRoller.Engine.API.Models.Requests;
using DiceRoller.Engine.API.Models.Responses;

namespace DiceRoller.Engine.API.Services.Abstractions
{
    public interface IDiceRollService
    {
        Task<DiceRollResponse> RollTheDiceAsync(User user);
        Task<PaginatedResult<DiceRollResponse>> GetAllPaginatedAsync(User user, GetDiceRollRequest request);
    }
}
