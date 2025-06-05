using DiceRoller.Domain.Exceptions;

namespace DiceRoller.Engine.API.Models.Responses
{
    public class BadRequestResponse
    {
        public BadRequestResponse(List<FailureError> errors)
        {
            Errors = errors;
        }

        public List<FailureError> Errors { get; set; }
    }
}
