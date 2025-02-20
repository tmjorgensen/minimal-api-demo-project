using Application.UseCases.Projects;
using Domain.Models;
using FastEndpoints;

namespace WebApi.Endpoints.Projects;

public class SearchProjectsEndpoint(
    ISearchProjectsUseCase useCase
    ) : EndpointWithoutRequest<ProjectSearchResponseDto>
{
    private readonly ISearchProjectsUseCase _useCase = useCase;

    public override void Configure()
    {
        Get("/projects");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var title = Query<string?>("title", false);
        var previousId = Query<Guid?>("previousId", false);
        var isDescending = Query<bool?>("desc", false);
        var rowCount = Query<int?>("take", false);

        var result = await _useCase.InvokeAsync(
            new(title, previousId, isDescending, rowCount),
            ct).ConfigureAwait(false);

        var response = new ProjectSearchResponseDto(result.Items, result.TotalCount);

        await SendOkAsync(response, ct).ConfigureAwait(false);
    }
}

public record ProjectSearchResponseDto(IEnumerable<Project> Items, int Count);
