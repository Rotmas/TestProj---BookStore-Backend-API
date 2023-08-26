using Model;
using System.Net;

namespace BookStore;

public class NotFoundExceptionMiddleware
{
    private readonly RequestDelegate Next;

    public NotFoundExceptionMiddleware(RequestDelegate next)
        => Next = next;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await Next(httpContext);
        }
        catch (NotFoundException)
        {
            HandleNotFoundException(httpContext);
        }
    }

    static void HandleNotFoundException(HttpContext context)
    {
        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
        context.Response.ContentType = "text/plain";
        context.Response.WriteAsync("Resource not found.");
    }
}