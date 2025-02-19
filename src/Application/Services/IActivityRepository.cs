using Application.Models;
using Application.UseCases.Activities;
using Domain.Models;

namespace Application.Services;
public interface IActivityRepository
{
    Task CreateAsync(Activity activity, CancellationToken ct);
    Task UpdateAsync(Activity activity, CancellationToken ct);
    Task<Activity> GetAsync(Guid activityId, CancellationToken ct);
    Task<SearchResponse<Activity>> SearchAsync(SearchActivitiesRequest request, CancellationToken ct);
    Task DeleteAsync(Guid activityId, CancellationToken ct);
}
