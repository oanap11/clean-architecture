using DoubleDinner.Application.Authentication.Common;
using DoubleDinner.Application.Authentication.Queries.Login;
using DoubleDinner.Application.Common.Interfaces.Authentication;
using DoubleDinner.Application.Common.Interfaces.Persistance;
using DoubleDinner.Domain.Common.Errors;
using DoubleDinner.Domain.Entities;
using ErrorOr;
using MediatR;

namespace DoubleDinner.Application.Authentication.Commands.Login;

public record LoginCommandHandler : 
    IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    
    public LoginCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        if(_userRepository.GetUserByEmail(query.Email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        if(user.Password != query.Password)
        {
            return new[] { Errors.Authentication.InvalidCredentials };
        }

        var token  = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user, 
            token);
    }
}
