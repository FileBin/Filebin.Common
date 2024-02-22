using Filebin.Common.Util.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Filebin.Common.Validation.Middleware;

internal sealed class ExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler {

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {
        httpContext.Response.StatusCode = GetStatusCode(exception);

        return await problemDetailsService.TryWriteAsync(
            new ProblemDetailsContext() {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = {
                    Title = GetTitle(exception),
                    Detail = exception.Message,
                    Type = exception.GetType().Name,
                },
            });
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };
    private static string GetTitle(Exception exception) =>
        exception switch {
            WebException innoShopException => innoShopException.Title,
            _ => "Internal Server Error"
        };
    private static IEnumerable<string>? GetErrors(Exception exception) {
        IEnumerable<string>? errors = null;
        if (exception is ValidationException validationException) {
            errors = validationException.Errors.Select(x => x.ErrorMessage);
        }
        return errors;
    }
}