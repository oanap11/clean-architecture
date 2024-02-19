using DoubleDinner.Application.Common.Interfaces.Authentication;
using DoubleDinner.Application.Common.Interfaces.Persistance;
using DoubleDinner.Domain.Common.Errors;
using DoubleDinner.Domain.Entities;
using ErrorOr;

namespace DoubleDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{   
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        if(_userRepository.GetUserByEmail(email) is not null)
        {
            //throw new Exception("User with given email already exists");
            return Errors.User.DuplicateEmail;
        }

        var user = new User 
        { 
            FirstName = firstName, 
            LastName = lastName, 
            Email = email, 
            Password = password 
        };

        _userRepository.Add(user);
        
        var token  = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user, 
            token);
    }

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        if(_userRepository.GetUserByEmail(email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        if(user.Password != password)
        {
            return new[] { Errors.Authentication.InvalidCredentials };
        }

        var token  = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user, 
            token);
    }
}