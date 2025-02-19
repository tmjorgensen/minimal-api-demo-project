using Application.Models.Exceptions;
using Application.Services;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Activities;

public interface IUpdateActivityUseCase
{
    Task InvokeAsync(UpdateActivityRequest request, CancellationToken ct);
}

public class UpdateActivityUseCase(
    ILogger<UpdateActivityUseCase> logger,
    IActivityRepository activityRepository
    ) : IUpdateActivityUseCase
{
    private readonly ILogger _logger = logger;
    private readonly IActivityRepository _activityRepository = activityRepository;

    public async Task InvokeAsync(UpdateActivityRequest request, CancellationToken ct)
    {
        Activity item;
        try
        {
            item = await _activityRepository.GetAsync(request.Id, ct).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting activity {Id}: {Error}", request.Id, ex.Message);

            throw new NotFoundException($"Activity {request.Id} not found.", ex);
        }

        item = item with
        {
            ProjectId = request.ProjectId ?? item.ProjectId,
            Title = request.Title ?? item.Title
        };

        try
        {
            await _activityRepository.UpdateAsync(item, ct).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while updating activity {Id}: {Error}", request.Id, ex.Message);

            throw;
        }
    }
}

public record UpdateActivityRequest(Guid Id, Guid? ProjectId, string? Title);

