using DoubleDinner.Domain.Entities;

namespace DoubleDinner.Application.Authentication.Common;

public record AuthenticationResult (
    User User,
    string Token);