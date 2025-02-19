using Application.UseCases.Activities;
using FastEndpoints;
using FluentValidation;

namespace WebApi.Endpoints.Activities;

public class UpdateActivityRequestValidator : Validator<UpdateActivityRequestDto>
{
    public UpdateActivityRequestValidator()
    {
        RuleFor(x => x.ProjectId).NotEmpty().When(m => string.IsNullOrWhiteSpace(m.Title)).WithMessage("Either ProjectId or Title must have a value!");
        RuleFor(x => x.Title).NotEmpty().When(m => m.ProjectId is null || m.ProjectId == Guid.Empty).WithMessage("Either ProjectId or Title must have a value!");
    }
}

public class UpdateActivityEndpoint(
    IUpdateActivityUseCase useCase): Endpoint<UpdateActivityRequestDto>
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
public record UpdateActivityRequestDto(Guid? ProjectId, string? Title);
