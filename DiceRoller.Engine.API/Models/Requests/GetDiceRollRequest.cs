using DiceRoller.Domain.Abstractions.Pagination;

namespace DiceRoller.Engine.API.Models.Requests
{
    public class GetDiceRollRequest : PaginationOptions
    {
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
    }
}
