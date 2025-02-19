using Application.UseCases.Activities;
using Application.UseCases.Identity;
using Application.UseCases.Projects;

namespace WebApi.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<ISignInUseCase, SignInUseCase>();

        services
            .AddTransient<ICreateProjectUseCase, CreateProjectUseCase>()
            .AddTransient<IDeleteProjectUseCase, DeleteProjectUseCase>()
            .AddTransient<IGetProjectUseCase, GetProjectUseCase>()
            .AddTransient<ISearchProjectsUseCase, SearchProjectsUseCase>()
            .AddTransient<IUpdateProjectUseCase, UpdateProjectUseCase>();

        services
            .AddTransient<ICreateActivityUseCase, CreateActivityUseCase>()
            .AddTransient<IDeleteActivityUseCase, DeleteActivityUseCase>()
            .AddTransient<IGetActivityUseCase, GetActivityUseCase>()
            .AddTransient<ISearchActivitiesUseCase, SearchActivitiesUseCase>()
            .AddTransient<IUpdateActivityUseCase, UpdateActivityUseCase>();

        return services;
    }
}
