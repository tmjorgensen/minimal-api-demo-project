using FastEndpoints;
using FluentValidation;

namespace WebApi.Endpoints.Activities.UpdateActivity;

public class UpdateActivityRequestValidator : Validator<UpdateActivityRequestDto>
{
    public UpdateActivityRequestValidator()
    {
        RuleFor(x => x.ProjectId).NotEmpty().When(m => string.IsNullOrWhiteSpace(m.Title)).WithMessage("Either ProjectId or Title must have a value!");
        RuleFor(x => x.Title).NotEmpty().When(m => m.ProjectId is null || m.ProjectId == Guid.Empty).WithMessage("Either ProjectId or Title must have a value!");
    }
}
