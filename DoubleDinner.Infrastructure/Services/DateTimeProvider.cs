using DoubleDinner.Application.Common.Interfaces.Services;

namespace DoubleDinner.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}