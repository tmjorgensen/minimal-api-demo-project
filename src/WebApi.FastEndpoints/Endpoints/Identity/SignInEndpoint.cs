using Application.UseCases.Identity;
using FastEndpoints;

namespace WebApi.Endpoints.Identity;

public class SignInEndpoint(
    ISignInUseCase useCase
    ) : Endpoint<SignInRequestDto, SignInResponseDto>
{
    private readonly ISignInUseCase _useCase = useCase;

    public override void Configure()
    {
        Post("/identity/sign-in");
        AllowAnonymous(); 
    }

    public override async Task HandleAsync(SignInRequestDto dto, CancellationToken ct)
    {
        var result = await _useCase.InvokeAsync(new(dto.UserName, dto.Password), ct).ConfigureAwait(false);

        var response = new SignInResponseDto(result.AccessToken);

        await SendOkAsync(response, ct).ConfigureAwait(false);
    }
}

public record SignInRequestDto(string UserName, string Password);
public record SignInResponseDto(string AccessToken);
