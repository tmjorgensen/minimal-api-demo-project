using Application.Services;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Projects;

public interface ICreateProjectUseCase
{
    Task<Guid> InvokeAsync(CreateProjectRequest request, CancellationToken ct);
}

public class CreateProjectUseCase(
    ILogger<CreateProjectRequest> logger,
    IProjectRepository projectRepository
    ) : ICreateProjectUseCase
{
    private readonly ILogger _logger = logger;
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<Guid> InvokeAsync(CreateProjectRequest request, CancellationToken ct)
    {
        var item = new Project(Guid.NewGuid(), request.Title);

        try
        {
            await _projectRepository.CreateAsync(item, ct).ConfigureAwait(false);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error creating project: {Error}", ex.Message);

            throw;
        }

        return item.Id;
    }
}

public record CreateProjectRequest(string Title);
