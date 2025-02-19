using Application.UseCases.Activities;
using Domain.Models;
using FastEndpoints;

namespace WebApi.Endpoints.Activities;

public class SearchActivitiesEndpoint(
    ISearchActivitiesUseCase useCase
    ) : EndpointWithoutRequest<ActivitySearchResponseDto>
{
    private readonly ISearchActivitiesUseCase _useCase = useCase;

    public override void Configure()
    {
        Get("/activities");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var projectId = Query<Guid>("projectId", false);
        var title = Query<string>("title", false);
        var previousId = Query<Guid>("previousId", false);
        var isDescending = Query<bool>("desc", false);
        var rowCount = Query<int>("take", false);

        var result = await _useCase.InvokeAsync(
            new(projectId, title, previousId, isDescending, rowCount),
            ct).ConfigureAwait(false);

        var response = new ActivitySearchResponseDto(result.Items, result.TotalCount);

        await SendOkAsync(response, ct).ConfigureAwait(false);
    }
}

public record ActivitySearchResponseDto(IEnumerable<Activity> Items, int Count);

