using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Task2.Errors
{
    public class HandleErrorException: IExceptionHandler
    {
        private readonly ILogger<HandleErrorException> logger;

        public HandleErrorException(ILogger<HandleErrorException> logger)
        {
            this.logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception, "catch Error", exception.Message);
            var ProblemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server Error",
                Detail = exception.Message,
            };
            httpContext.Response.StatusCode = ProblemDetails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(ProblemDetails);
            return true;
        }
    }
}
