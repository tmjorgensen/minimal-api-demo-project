using Application.UseCases.Activities;
using FastEndpoints;

namespace WebApi.Endpoints.Activities.CreateActivity;

public class CreateActivityEndpoint(
    ICreateActivityUseCase useCase) : Endpoint<CreateActivityRequestDto>
{
    private readonly ICreateActivityUseCase _useCase = useCase;

    public override void Configure()
    {
        Post("/activities");
    }

    public override async Task HandleAsync(CreateActivityRequestDto req, CancellationToken ct)
    {
        var newId = await _useCase.InvokeAsync(new(req.ProjectId, req.Title), ct).ConfigureAwait(false);

        await SendCreatedAtAsync<GetActivityEndpoint>(
            new { Id = newId },
            new CreateActivityResponseDto(newId),
            cancellation: ct
            ).ConfigureAwait(false);
    }
}
