using Application.Services;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Activities;

public interface ICreateActivityUseCase
{
    Task<Guid> InvokeAsync(CreateActivityRequest request, CancellationToken ct);
}

public class CreateActivityUseCase(
    ILogger<CreateActivityUseCase> logger,
    IActivityRepository activityRepository
    ) : ICreateActivityUseCase
{
    private readonly ILogger _logger = logger;
    private readonly IActivityRepository _activityRepository = activityRepository;

    public async Task<Guid> InvokeAsync(CreateActivityRequest request, CancellationToken ct)
    {
        var item = new Activity(Guid.NewGuid(), request.ProjectId, request.Title);

        try
        {
            await _activityRepository.CreateAsync(item, ct).ConfigureAwait(false);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error while creating activity: {Error}", ex.Message);

            throw;
        }

        return item.Id;
    }
}

public record CreateActivityRequest(Guid ProjectId, string Title);
