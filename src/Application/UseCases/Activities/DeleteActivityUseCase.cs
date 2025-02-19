using Application.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Activities;

public interface IDeleteActivityUseCase
{
    Task InvokeAsync(Guid id, CancellationToken ct);
}

public class DeleteActivityUseCase(
    ILogger<DeleteActivityUseCase> logger,
    IActivityRepository activityRepository
    ) : IDeleteActivityUseCase
{
    private readonly ILogger _logger = logger;
    private readonly IActivityRepository _activityRepository = activityRepository;

    public async Task InvokeAsync(Guid id, CancellationToken ct)
    {
        try
        {
            await _activityRepository.DeleteAsync(id, ct).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while deleting activity {Id}: {Error}", id, ex.Message);

            throw;
        }
    }
}
