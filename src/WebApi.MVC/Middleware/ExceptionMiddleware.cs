using Application.Models.Exceptions;
using System.Net;

namespace WebApi.Middleware;
public class ExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            var status = "Internal Server Error";

            if (ex is NotFoundException)
            {
                status = "Not Found!";
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(new
            {
                Status = status,
                Code = httpContext.Response.StatusCode,
                Reason = ex.Message
            });
        }
    }
}

