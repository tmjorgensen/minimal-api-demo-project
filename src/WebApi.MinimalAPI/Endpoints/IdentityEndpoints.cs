using Application.UseCases.Identity;
using WebApi.DTO.Requests;
using WebApi.DTO.Responses;

namespace WebApi.Endpoints;

public static class IdentityEndpoints
{
    public static IEndpointRouteBuilder UseIdentityEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/identity");

        group.MapPost("/sign-in", static async (SignInRequestDto req, ISignInUseCase useCase, CancellationToken ct) =>
        {
            var response = await useCase.InvokeAsync(new(req.UserName, req.Password), ct).ConfigureAwait(false);

            return Results.Ok(new SignInResponseDto(response.AccessToken));
        });

        return builder;
    }
}
