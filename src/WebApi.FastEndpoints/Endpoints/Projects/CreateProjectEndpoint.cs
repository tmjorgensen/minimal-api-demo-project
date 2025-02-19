using Application.UseCases.Projects;
using FastEndpoints;
using FluentValidation;

namespace WebApi.Endpoints.Projects;

public class CreateProjectRequestValidator : Validator<CreateProjectRequestDto>
{
    public CreateProjectRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty!");
    }
}

public class CreateProjectEndpoint(
    ICreateProjectUseCase useCase) : Endpoint<CreateProjectRequestDto>
{
    private readonly ICreateProjectUseCase _useCase = useCase;

    public override void Configure()
    {
        Post("/projects");
    }

    public override async Task HandleAsync(CreateProjectRequestDto req, CancellationToken ct)
    {
        var newId = await _useCase.InvokeAsync(new(req.Title), ct).ConfigureAwait(false);

        await SendCreatedAtAsync<GetProjectEndpoint>(new { Id = newId }, new CreateProjectResponseDto(newId), cancellation: ct).ConfigureAwait(false);
    }
}

public record CreateProjectRequestDto(string Title);
public record CreateProjectResponseDto(Guid Id);
