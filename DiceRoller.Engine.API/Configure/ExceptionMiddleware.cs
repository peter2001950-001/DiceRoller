using System.Text.Json;
using DiceRoller.Domain.Exceptions;
using DiceRoller.Engine.API.Models.Constats;
using DiceRoller.Engine.API.Models.Responses;

namespace DiceRoller.Engine.API.Configure
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); // Proceed to the next middleware
            }
            catch (FailureException ex)
            {
                await HandleFailureExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleFailureExceptionAsync(HttpContext context, FailureException exception)
        {
            var statusCode = exception.GetStatusCode();
            var result = JsonSerializer.Serialize(new BadRequestResponse(exception.GetFailureErrors()));

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(result);
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = 500;
            var result = JsonSerializer.Serialize(new BadRequestResponse([
                new FailureError(FailureErrorTypes.InternalError, "Something went wrong, try again later")
            ]));

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            _logger.LogError(exception, exception.Message);

            return context.Response.WriteAsync(result);
        }
    }
}
