using System.Net;

namespace DiceRoller.Domain.Exceptions
{
    public class NotFoundException : FailureException
    {
        public NotFoundException(FailureError failureError) : base(failureError)
        {
        }

        public NotFoundException(List<FailureError> failures) : base(failures)
        {
        }

        protected override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    }
}
