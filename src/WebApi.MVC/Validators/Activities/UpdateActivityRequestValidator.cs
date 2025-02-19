using FluentValidation;
using WebApi.DTO.Requests;

namespace WebApi.Validators.Activities;
public class UpdateActivityRequestValidator : AbstractValidator<UpdateActivityRequestDto>
{
    public UpdateActivityRequestValidator()
    {
        RuleFor(x => x.ProjectId).NotNull().When(m => string.IsNullOrWhiteSpace(m.Title)).WithMessage("Either ProjectId or Title must have a value!");
        RuleFor(x => x.Title).NotEmpty().When(m => m.ProjectId is null).WithMessage("Either ProjectId or Title must have a value!");
    }
}
