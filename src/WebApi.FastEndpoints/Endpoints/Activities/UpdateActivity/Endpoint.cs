using Application.UseCases.Activities;
using FastEndpoints;

namespace WebApi.Endpoints.Activities.UpdateActivity;

public class UpdateActivityEndpoint(
    IUpdateActivityUseCase useCase) : Endpoint<UpdateActivityRequestDto>
{
    private readonly IUpdateActivityUseCase _useCase = useCase;

    public override void Configure()
    {
        Put("/activities/{id}");
    }

    public override async Task HandleAsync(UpdateActivityRequestDto req, CancellationToken ct)
    {
        var id = Route<Guid>("id");

        await _useCase.InvokeAsync(new(id, req.ProjectId, req.Title), ct).ConfigureAwait(false);

        await SendNoContentAsync(ct).ConfigureAwait(false);
    }
}
