using Application.UseCases.Projects;
using Domain.Models;
using FastEndpoints;

namespace WebApi.Endpoints.Projects;

public class GetProjectEndpoint(
    IGetProjectUseCase useCase) : EndpointWithoutRequest<Project>
{
    private readonly IGetProjectUseCase _useCase = useCase;

    public override void Configure()
    {
        Get("/projects/{id}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");

        var item = await _useCase.InvokeAsync(id, ct).ConfigureAwait(false);

        await SendOkAsync(item, ct).ConfigureAwait(false);
    }
}
