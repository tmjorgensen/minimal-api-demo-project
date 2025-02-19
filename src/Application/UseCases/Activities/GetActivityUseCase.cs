using Application.Models.Exceptions;
using Application.Services;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Activities;

public interface IGetActivityUseCase
{
    Task<Activity> InvokeAsync(Guid id, CancellationToken ct);
}

public class GetActivityUseCase(
    ILogger<GetActivityUseCase> logger,
    IActivityRepository activityRepository
    ) : IGetActivityUseCase
{
    private readonly ILogger _logger = logger;
    private readonly IActivityRepository _activityRepository = activityRepository;

    public async Task<Activity> InvokeAsync(Guid id, CancellationToken ct)
    {
        try
        {
            return await _activityRepository.GetAsync(id, ct).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting activity {Id}: {Error}", id, ex.Message);

            throw new NotFoundException($"Activity {id} not found.", ex);
        }
    }
}
