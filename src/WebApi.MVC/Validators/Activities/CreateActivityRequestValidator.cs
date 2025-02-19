using FluentValidation;
using WebApi.DTO.Requests;

namespace WebApi.Validators.Activities;
public class CreateActivityRequestValidator : AbstractValidator<CreateActivityRequestDto>
{
    public CreateActivityRequestValidator()
    {
        RuleFor(x => x.ProjectId).NotNull().WithMessage("ProjectId cannot be null!");
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty!");
    }
}
