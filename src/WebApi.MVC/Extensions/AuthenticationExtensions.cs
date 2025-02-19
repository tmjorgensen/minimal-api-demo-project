using Application.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApi.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddWebApiAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<AuthConfiguration>(options
            => configuration.GetSection("Auth").Bind(options)
        );

        services.AddAuthentication()
            .AddJwtBearer(options =>
            {
                var provider = services.BuildServiceProvider();

                var authConfiguration = provider.GetRequiredService<IOptions<AuthConfiguration>>().Value;

                options.TokenValidationParameters = new()
                {
                    ValidIssuer = authConfiguration.Issuer,
                    ValidAudience = authConfiguration.Audience,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(authConfiguration.Secret)
                    )
                };
            });

        services.AddAuthorization();

        return services;
    }
}
