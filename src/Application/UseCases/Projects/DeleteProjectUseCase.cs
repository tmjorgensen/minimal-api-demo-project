using Application.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Projects;

public interface IDeleteProjectUseCase
{
    Task InvokeAsync(Guid id, CancellationToken ct);
}

public class DeleteProjectUseCase(
    ILogger<DeleteProjectUseCase> logger,
    IProjectRepository projectRepository
    ) : IDeleteProjectUseCase
{
    private readonly ILogger _logger = logger;
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task InvokeAsync(Guid id, CancellationToken ct)
    {
        try
        {
            await _projectRepository.DeleteAsync(id, ct).ConfigureAwait(false);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error while deleting project {Id}: {Error}", id, ex.Message);

            throw;
        }
    }
}
