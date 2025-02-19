using Application.Models;
using Application.UseCases.Projects;
using Domain.Models;

namespace Application.Services;
public interface IProjectRepository
{
    Task CreateAsync(Project project, CancellationToken ct);
    Task UpdateAsync(Project project, CancellationToken ct);
    Task<Project> GetAsync(Guid projectId, CancellationToken ct);
    Task<SearchResponse<Project>> SearchAsync(SearchProjectsRequest request, CancellationToken ct);
    Task DeleteAsync(Guid projectId, CancellationToken ct);
}
