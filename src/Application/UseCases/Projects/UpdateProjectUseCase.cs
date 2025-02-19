using Application.Models.Exceptions;
using Application.Services;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Projects;

public interface IUpdateProjectUseCase
{
    Task InvokeAsync(UpdateProjectRequest request, CancellationToken ct);
}

public class UpdateProjectUseCase(
    ILogger<UpdateProjectUseCase> logger,
    IProjectRepository projectRepository
    ) : IUpdateProjectUseCase
{
    private readonly ILogger _logger = logger;
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task InvokeAsync(UpdateProjectRequest request, CancellationToken ct)
    {
        Project item;
        try
        {
            item = await _projectRepository.GetAsync(request.Id, ct).ConfigureAwait(false);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error while getting project {Id}: {Error}", request.Id, ex.Message);

            throw new NotFoundException($"Project {request.Id} not found.", ex);
        }

        item = item with 
        { 
            Title = request.Title ?? item.Title 
        };

        try
        {
            await _projectRepository.UpdateAsync(item ,ct).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while updating project {Id}: {Error}", request.Id, ex.Message);

            throw;
        }
    }
}

public record UpdateProjectRequest(Guid Id, string? Title);
