using Application.UseCases.Activities;
using FastEndpoints;
using FluentValidation;

namespace WebApi.Endpoints.Activities;

public class CreateActivityRequestValidator : Validator<CreateActivityRequestDto>
{
    public CreateActivityRequestValidator()
    {
        RuleFor(x => x.ProjectId).NotEmpty().WithMessage("ProjectId cannot be empty!");
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty!");
    }
}

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

public record CreateActivityRequestDto(Guid ProjectId, string Title);
public record CreateActivityResponseDto(Guid Id);
