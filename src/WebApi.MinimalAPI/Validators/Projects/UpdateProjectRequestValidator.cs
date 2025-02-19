using FluentValidation;
using WebApi.DTO.Requests;

namespace WebApi.Validators.Projects;
public class UpdateProjectRequestValidator : AbstractValidator<UpdateProjectRequestDto>
{
    public UpdateProjectRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty!");
    }
}
