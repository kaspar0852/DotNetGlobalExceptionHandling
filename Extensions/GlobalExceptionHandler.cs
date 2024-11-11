using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ExceptionHandlerPOC.Extensions;

internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IWebHostEnvironment _env;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IWebHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var correlationId = Guid.NewGuid().ToString();
        httpContext.Response.ContentType = "application/problem+json";

        var problemDetails = new ProblemDetails
        {
            Type = GetProblemType(exception),   
            Title = GetProblemTitle(exception),
            Status = GetStatusCode(exception),
            Detail = _env.IsDevelopment() ? exception.ToString() : exception.Message,
            Instance = httpContext.Request.Path,
            Extensions = { { "traceId", correlationId } }
        };
        
        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions.Add("errors", validationException.Errors);
        }

        _logger.LogError(exception, "An error occurred while processing {Path}. TraceId: {TraceId}", httpContext.Request.Path, correlationId);

        httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);

        return true;
    }

    private static int GetStatusCode(Exception exception) => exception switch
    {
        NotFoundException => StatusCodes.Status404NotFound,
        ValidationException => StatusCodes.Status400BadRequest,
        _ => StatusCodes.Status500InternalServerError
    };

    private static string GetProblemTitle(Exception exception) => exception switch
    {
        NotFoundException => "Resource not found",
        ValidationException => "Validation error",
        _ => "An unexpected error occurred"
    };

    private static string GetProblemType(Exception exception) => exception switch
    {
        NotFoundException => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
        ValidationException => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
        _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
    };

}

// Custom exception classes
public abstract class NotFoundException : Exception
{
    protected NotFoundException(string message) : base(message) { }
}

public abstract class ValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    protected ValidationException(string message, IDictionary<string, string[]> errors) : base(message)
    {
        Errors = errors;
    }
}
