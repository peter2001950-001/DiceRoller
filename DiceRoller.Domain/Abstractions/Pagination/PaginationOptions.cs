namespace DiceRoller.Domain.Abstractions.Pagination
{
    public class PaginationOptions
    {
        public int StartAt { get; set; } = 0;
        public int Count { get; set; } = 100;
        public IEnumerable<string>? SortFields { get; set; } = null;
    }
}
