using Application.UseCases.Projects;
using FastEndpoints;

namespace WebApi.Endpoints.Projects;

public class DeleteProjectEndpoint(
    IDeleteProjectUseCase useCase) : EndpointWithoutRequest
{
    private readonly IDeleteProjectUseCase _useCase = useCase;

    public override void Configure()
    {
        Delete("/projects/{id}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");

        await _useCase.InvokeAsync(id, ct).ConfigureAwait(false);

        await SendNoContentAsync(ct).ConfigureAwait(false);
    }
}
