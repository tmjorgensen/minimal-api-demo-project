using Application.UseCases.Activities;
using Domain.Models;
using WebApi.DTO.Requests;
using WebApi.DTO.Responses;

namespace WebApi.Endpoints;

public static class ActivityEndpoints
{
    private const string GetResource = "GetActivity";

    public static IEndpointRouteBuilder UseActivityEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/activities");

        group.MapGet("/", static async (
            Guid? projectId, string? title, Guid? previousId, bool? desc, int? take,
            ISearchActivitiesUseCase useCase, CancellationToken ct) =>
        {
            var result = await useCase.InvokeAsync(
                new(projectId, title, previousId, desc ?? false, take),
                ct
                ).ConfigureAwait(false);

            var response = new SearchResponseDto<Activity>(result.Items, result.TotalCount);

            return Results.Ok(response);
        });

        group.MapPost("/", static async (CreateActivityRequestDto req, ICreateActivityUseCase useCase, CancellationToken ct) =>
        {
            var newId = await useCase.InvokeAsync(new(req.ProjectId, req.Title), ct).ConfigureAwait(false);

            return Results.CreatedAtRoute(GetResource, new { Id = newId }, new CreatedDto(newId));
        });

        group.MapGet("/{id}", static async(Guid id, IGetActivityUseCase useCase, CancellationToken ct) =>
        {
            var item = await useCase.InvokeAsync(id, ct).ConfigureAwait(false);

            return Results.Ok(item);
        }).WithName(GetResource);

        group.MapPut("/{id}", static async (Guid id, UpdateActivityRequestDto req, IUpdateActivityUseCase useCase, CancellationToken ct) =>
        {
            await useCase.InvokeAsync(new(id, req.ProjectId, req.Title), ct).ConfigureAwait(false);

            return Results.NoContent();
        });

        group.MapDelete("/{id}", static async (Guid id, IDeleteActivityUseCase useCase, CancellationToken ct) =>
        {
            await useCase.InvokeAsync(id, ct).ConfigureAwait(false);

            return Results.NoContent();
        });

        return builder;
    }
}
