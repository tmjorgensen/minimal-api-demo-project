using Application.Models;
using Application.UseCases.Activities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.UseCases.Identity;

public interface ISignInUseCase
{
    Task<SignInResponse> InvokeAsync(SignInRequest request, CancellationToken ct);
}

public class SignInUseCase(
    ILogger<SignInUseCase> logger,
    IOptions<AuthConfiguration> authConfiguration
    ) : ISignInUseCase
{
    private readonly ILogger _logger = logger;
    private readonly IOptions<AuthConfiguration> _authConfiguration = authConfiguration;

    public Task<SignInResponse> InvokeAsync(SignInRequest request, CancellationToken ct)
    {
        try
        {
            var authConfig = _authConfiguration.Value;

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(authConfig.Secret)
            );

            var claims = new List<Claim>
        {
            new("sub", request.UserName)
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = authConfig.Issuer,
                Audience = authConfig.Audience,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    securityKey,
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            var response = new SignInResponse(accessToken);

            return Task.FromResult(response);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error while signing in: {Error}", ex.Message);

            throw;
        }
    }
}

public record SignInRequest(string UserName, string Password);
public record SignInResponse(string AccessToken);