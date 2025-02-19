using FluentValidation;
using WebApi.DTO.Requests;

namespace WebApi.Validators.Identity;

public class SignInRequestValidator : AbstractValidator<SignInRequestDto>
{
    public SignInRequestValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName cannot be empty!");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty!");
    }
}
