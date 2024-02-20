using DoubleDinner.Application.Services.Authentication.Common;
using ErrorOr;

namespace DoubleDinner.Application.Services.Authentication.Queries;

public interface IAuthenticationQueryService
{     
    ErrorOr<AuthenticationResult> Login(string email, string password);
}