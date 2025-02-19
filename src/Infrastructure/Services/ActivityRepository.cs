using Application.Models;
using Application.Services;
using Application.UseCases.Activities;
using Domain.Models;

namespace Infrastructure.Services;
public class ActivityRepository : RepositoryBase<Activity>, IActivityRepository
{
    public Task<SearchResponse<Activity>> SearchAsync(SearchActivitiesRequest request, CancellationToken ct)
    {
        var query = Items.AsQueryable();

        if (request.ProjectId != null)
            query = query.Where(x => x.ProjectId == request.ProjectId.Value);

        if (request.Title != null)
            query = query.Where(x => x.Title.Contains(request.Title, StringComparison.OrdinalIgnoreCase));

        return SearchAsync(query, request, ct);
    }
}
