using System.Net;

namespace DiceRoller.Domain.Exceptions
{
    public class BadRequestException : FailureException
    {
        public BadRequestException(FailureError failureError) : base(failureError)
        {
        }

        public BadRequestException(List<FailureError> failures) : base(failures)
        {
        }

        protected override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    }
}
