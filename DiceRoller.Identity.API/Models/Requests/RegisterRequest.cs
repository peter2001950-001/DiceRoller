namespace DiceRoller.Identity.API.Models.Requests
{
    public record RegisterRequest(string FirstName, string LastName, string Email, string Avatar, string Password)
    {
    }
}
