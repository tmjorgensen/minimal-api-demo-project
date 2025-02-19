using Application.Models.Exceptions;
using FastEndpoints;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace WebApi.Extensions;

public static class ExceptionHandlerExtensions
{
    public static IApplicationBuilder UseCustomExceptionhandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(
            errApp =>
            {
                errApp.Run(
                    async ctx =>
                    {
                        var exHandlerFeature = ctx.Features.Get<IExceptionHandlerFeature>();

                        if (exHandlerFeature != null)
                        {
                            var ex = exHandlerFeature.Error;

                            var status = "Internal Server Error";
                            if (ex is NotFoundException)
                            {
                                ctx.Response.StatusCode = (int)HttpStatusCode.NotFound;
                                status = "Not Found";
                            }
                            else
                                ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                            ctx.Response.ContentType = "application/problem+json";
                            await ctx.Response.WriteAsJsonAsync(
                                new InternalErrorResponse
                                {
                                    Status = status,
                                    Code = ctx.Response.StatusCode,
                                    Reason = ex.Message
                                });
                        }
                    }
                );
            });

        return app;
    }
}
