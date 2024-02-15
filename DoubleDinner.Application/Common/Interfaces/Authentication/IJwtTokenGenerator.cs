using DoubleDinner.Domain.Entities;

namespace DoubleDinner.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator 
{
    string GenerateToken(User user);
}