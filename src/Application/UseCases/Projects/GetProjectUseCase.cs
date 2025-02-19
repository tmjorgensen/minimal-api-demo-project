using Application.Models.Exceptions;
using Application.Services;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Projects;

public interface IGetProjectUseCase
{
    Task<Project> InvokeAsync(Guid id, CancellationToken ct);
}

public class GetProjectUseCase(
    ILogger<GetProjectUseCase> logger,
    IProjectRepository projectRepository
    ) : IGetProjectUseCase
{
    private readonly ILogger _logger = logger;
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<Project> InvokeAsync(Guid id, CancellationToken ct)
    {
        try
        {
            return await _projectRepository.GetAsync(id, ct).ConfigureAwait(false);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error while getting project {Id}: {Error}", id, ex.Message);

            throw new NotFoundException($"Project {id} not found.", ex);
        }
    }
}
