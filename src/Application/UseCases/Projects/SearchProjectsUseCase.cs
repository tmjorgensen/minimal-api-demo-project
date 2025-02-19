using Application.Models;
using Application.Services;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Projects;

public interface ISearchProjectsUseCase
{
    Task<SearchResponse<Project>> InvokeAsync(SearchProjectsRequest request, CancellationToken ct);
}

public class SearchProjectsUseCase(
    ILogger<SearchProjectsUseCase> logger,
    IProjectRepository projectRepository
    ) : ISearchProjectsUseCase
{
    private readonly ILogger _logger = logger;
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<SearchResponse<Project>> InvokeAsync(SearchProjectsRequest request, CancellationToken ct)
    {
        try
        {
            return await _projectRepository.SearchAsync(request, ct).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while searching projects: {Error}", ex.Message);
            throw;
        }
    }
}

public record SearchProjectsRequest(string? Title, Guid? PreviousId, bool? IsDescending, int? RowCount)
    : SearchRequest(PreviousId, IsDescending, RowCount);
