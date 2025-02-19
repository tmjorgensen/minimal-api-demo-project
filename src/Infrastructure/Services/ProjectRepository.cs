using Application.Models;
using Application.Services;
using Application.UseCases.Projects;
using Domain.Models;

namespace Infrastructure.Services;
public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
{
    public Task<SearchResponse<Project>> SearchAsync(SearchProjectsRequest request, CancellationToken ct)
    {
        var query = Items.AsQueryable();

        if (request.Title != null)
            query = query.Where(x => x.Title.Contains(request.Title, StringComparison.OrdinalIgnoreCase));

        return SearchAsync(query, request, ct);
    }
}
