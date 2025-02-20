using Application.UseCases.Activities;
using Domain.Models;
using FastEndpoints;

namespace WebApi.Endpoints.Activities.GetActivity;

public class GetActivityEndpoint(
    IGetActivityUseCase useCase) : EndpointWithoutRequest<Activity>
{
    private readonly IGetActivityUseCase _useCase = useCase;

    public override void Configure()
    {
        Get("/activities/{id}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");

        var item = await _useCase.InvokeAsync(id, ct).ConfigureAwait(false);

        await SendOkAsync(item, ct).ConfigureAwait(false);
    }
}
