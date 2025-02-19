using FluentValidation;
using WebApi.DTO.Requests;
using WebApi.Validators.Activities;
using WebApi.Validators.Identity;
using WebApi.Validators.Projects;

namespace WebApi.Extensions;

public static class ValidatorExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services
            .AddTransient<IValidator<CreateActivityRequestDto>, CreateActivityRequestValidator>()
            .AddTransient<IValidator<UpdateActivityRequestDto>, UpdateActivityRequestValidator>()
            .AddTransient<IValidator<SignInRequestDto>, SignInRequestValidator>()
            .AddTransient<IValidator<CreateProjectRequestDto>, CreateProjectRequestValidator>()
            .AddTransient<IValidator<UpdateProjectRequestDto>, UpdateProjectRequestValidator>();

        return services;
    }
}
