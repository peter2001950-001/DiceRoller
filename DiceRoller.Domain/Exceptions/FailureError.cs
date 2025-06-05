namespace DiceRoller.Domain.Exceptions
{
    public record FailureError(string Type, string Message, string? Field = null)
    {
    }
}
