using DoubleDinner.Application.Common.Interfaces.Authentication;
using DoubleDinner.Application.Common.Interfaces.Services;
using DoubleDinner.Infrastructure.Authentication;
using DoubleDinner.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DoubleDinner.Application.Common.Interfaces.Persistance;
using DoubleDinner.Infrastructure.Persistance;

namespace DoubleDinner.Infrastructure;

public static class DependencyInjection 
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IUserRepository, UserRepository>();
        
        return services; 
    }
}