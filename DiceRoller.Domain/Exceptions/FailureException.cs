using System.Net;

namespace DiceRoller.Domain.Exceptions
{
    public abstract class FailureException : Exception
    {
        private List<FailureError> failures = new();
        protected abstract HttpStatusCode StatusCode { get; }

        public FailureException(FailureError failureError)
        {
            failures.Add(failureError);
        }

        public FailureException(List<FailureError> failures)
        {
            this.failures = failures;
        }

        public List<FailureError> GetFailureErrors() => failures;

        public HttpStatusCode GetStatusCode() => StatusCode;
    }
}
