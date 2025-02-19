using FluentValidation;
using WebApi.DTO.Requests;

namespace WebApi.Validators.Projects;
public class CreateProjectRequestValidator : AbstractValidator<CreateProjectRequestDto>
{
    public CreateProjectRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty!");
    }
}
