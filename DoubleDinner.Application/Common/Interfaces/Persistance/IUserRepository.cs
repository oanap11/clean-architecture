using DoubleDinner.Domain.Entities;

namespace DoubleDinner.Application.Common.Interfaces.Persistance;

public interface IUserRepository 
{
    User? GetUserByEmail(string email);

    void Add(User user);
}