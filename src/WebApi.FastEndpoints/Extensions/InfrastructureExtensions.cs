using Application.Services;
using Infrastructure.Services;

namespace WebApi.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services
            .AddSingleton<IActivityRepository, ActivityRepository>()
            .AddSingleton<IProjectRepository, ProjectRepository>();

        return services;
    }
}
