using DoubleDinner.Domain.Entities;

namespace DoubleDinner.Application.Services.Authentication;

public record AuthenticationResult (
    User User,
    string Token);