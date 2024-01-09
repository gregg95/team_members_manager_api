using Domain.Errors;
using Newtonsoft.Json;
using System.Net;
using InvalidOperationException = Domain.Errors.InvalidOperationException;

namespace WebAPI.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound);
        }
        catch (InvalidOperationException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.Conflict);
        }
        catch (TimeoutException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.GatewayTimeout);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode code)
    {
        var result = JsonConvert.SerializeObject(new { error = exception.Message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}
