using Application.UseCases.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.Requests;
using WebApi.DTO.Responses;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class IdentityController(
    ISignInUseCase signinuseCase
    ) : ControllerBase
{
    private readonly ISignInUseCase _signInUseCase = signinuseCase;

    [HttpPost]
    [Route("sign-in")]
    public async Task<IActionResult> SignIn(SignInRequestDto req, CancellationToken ct)
    {
        var response = await _signInUseCase.InvokeAsync(new(req.UserName, req.Password), ct).ConfigureAwait(false);

        return Ok(new SignInResponseDto(response.AccessToken));
    }

}
