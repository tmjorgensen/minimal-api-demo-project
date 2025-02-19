using Application.UseCases.Projects;
using FastEndpoints;
using FluentValidation;

namespace WebApi.Endpoints.Projects;

public class UpdateProjectRequestValidator : Validator<UpdateProjectRequestDto>
{
    public UpdateProjectRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty!");
    }
}

public class UpdateProjectEndpoint(
    IUpdateProjectUseCase useCase) : Endpoint<UpdateProjectRequestDto>
{
    private readonly IUpdateProjectUseCase _useCase = useCase;

    public override void Configure()
    {
        Put("/projects/{id}");
    }

    public override async Task HandleAsync(UpdateProjectRequestDto req, CancellationToken ct)
    {
        var id = Route<Guid>("id");

        await _useCase.InvokeAsync(new(id, req.Title), ct).ConfigureAwait(false);

        await SendNoContentAsync(ct).ConfigureAwait(false);
    }
}

public record UpdateProjectRequestDto(string Title);
