using Application.UseCases.Projects;
using Domain.Models;
using WebApi.DTO.Requests;
using WebApi.DTO.Responses;

namespace WebApi.Endpoints;

public static class ProjectEndpoints
{
    private const string GetResource = "GetProject";

    public static IEndpointRouteBuilder UseProjectEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/projects");

        group.MapGet("/", static async (
            string? title, Guid? previousId, bool? desc, int? take,
            ISearchProjectsUseCase useCase, CancellationToken ct) =>
        {
            var result = await useCase.InvokeAsync(
                new(title, previousId, desc, take),
                ct
                ).ConfigureAwait(false);

            var response = new SearchResponseDto<Project>(result.Items, result.TotalCount);

            return Results.Ok(response);
        });

        group.MapPost("/", static async (CreateProjectRequestDto req, ICreateProjectUseCase useCase, CancellationToken ct) =>
        {
            var newId = await useCase.InvokeAsync(new(req.Title), ct).ConfigureAwait(false);

            return Results.CreatedAtRoute(GetResource, new { Id = newId }, new CreatedDto(newId));
        });

        group.MapGet("/{id}", static async (Guid id, IGetProjectUseCase useCase, CancellationToken ct) =>
        {
            var item = await useCase.InvokeAsync(id, ct).ConfigureAwait(false);

            return Results.Ok(item);
        }).WithName(GetResource);

        group.MapPut("/{id}", static async (Guid id, UpdateProjectRequestDto req, IUpdateProjectUseCase useCase, CancellationToken ct) =>
        {
            await useCase.InvokeAsync(new(id, req.Title), ct).ConfigureAwait(false);

            return Results.NoContent();
        });

        group.MapDelete("/{id}", static async (Guid id, IDeleteProjectUseCase useCase, CancellationToken ct) =>
        {
            await useCase.InvokeAsync(id, ct).ConfigureAwait(false);

            return Results.NoContent();
        });

        return builder;
    }
}
