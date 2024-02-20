using DoubleDinner.Contract.Authentication;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using DoubleDinner.Application.Services.Authentication.Commands;
using DoubleDinner.Application.Services.Authentication.Common;
using DoubleDinner.Application.Services.Authentication.Queries;

namespace DoubleDinner.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController 
{
    private readonly IAuthenticationCommandService _authenticationCommandService;
    
    private readonly IAuthenticationQueryService _authenticationQueryService;
    
    public AuthenticationController(
        IAuthenticationCommandService authenticationCommandService,
        IAuthenticationQueryService authenticationQueryService)
    {
        _authenticationCommandService = authenticationCommandService;
        _authenticationQueryService = authenticationQueryService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = _authenticationCommandService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors)
        );
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationQueryService.Login(
            request.Email,
            request.Password);
        
         return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors)
        );
        
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
                    authResult.User.Id,
                    authResult.User.FirstName,
                    authResult.User.LastName,
                    authResult.User.Email,
                    authResult.Token);
    }
}