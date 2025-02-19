using Application.Models;
using Application.Services;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Activities;

public interface ISearchActivitiesUseCase
{
    Task<SearchResponse<Activity>> InvokeAsync(SearchActivitiesRequest request, CancellationToken ct);
}

public class SearchActivitiesUseCase(
    ILogger<SearchActivitiesUseCase> logger,
    IActivityRepository activityRepository
    ) : ISearchActivitiesUseCase
{
    private readonly ILogger _logger = logger;
    private readonly IActivityRepository _activityRepository = activityRepository;

    public async Task<SearchResponse<Activity>> InvokeAsync(SearchActivitiesRequest request, CancellationToken ct)
    {
        try
        {
            return await _activityRepository.SearchAsync(request, ct).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while searching activities: {Error}", ex.Message);

            throw;
        }
    }
}

public record SearchActivitiesRequest(Guid? ProjectId, string? Title, Guid? PreviousId, bool? IsDescending, int? RowCount)
    : SearchRequest(PreviousId, IsDescending, RowCount);
