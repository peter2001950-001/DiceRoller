namespace DiceRoller.Identity.API.Models.Responses
{
    public class TokenResponse
    {
        public TokenResponse(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}
