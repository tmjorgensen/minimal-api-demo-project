using Application.UseCases.Activities;
using FastEndpoints;

namespace WebApi.Endpoints.Activities;

public class DeleteActivityEndpoint(
    IDeleteActivityUseCase useCase) : EndpointWithoutRequest
{
    private readonly IDeleteActivityUseCase _useCase = useCase;

    public override void Configure()
    {
        Delete("/activities/{id}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");

        await _useCase.InvokeAsync(id, ct).ConfigureAwait(false);

        await SendNoContentAsync(ct).ConfigureAwait(false);
    }
}
