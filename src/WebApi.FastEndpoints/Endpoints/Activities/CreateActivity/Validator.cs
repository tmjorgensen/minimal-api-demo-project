using FastEndpoints;
using FluentValidation;

namespace WebApi.Endpoints.Activities.CreateActivity;

public class CreateActivityRequestValidator : Validator<CreateActivityRequestDto>
{
    public CreateActivityRequestValidator()
    {
        RuleFor(x => x.ProjectId).NotEmpty().WithMessage("ProjectId cannot be empty!");
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty!");
    }
}
