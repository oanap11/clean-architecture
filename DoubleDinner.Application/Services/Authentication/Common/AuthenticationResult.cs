using DoubleDinner.Domain.Entities;

namespace DoubleDinner.Application.Services.Authentication.Common;

public record AuthenticationResult (
    User User,
    string Token);