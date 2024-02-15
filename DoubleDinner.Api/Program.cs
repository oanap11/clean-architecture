using DoubleDinner.Api.Common.Errors;
using DoubleDinner.Application;
using DoubleDinner.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
        
    builder.Services.AddControllers();

    builder.Services.AddSingleton<ProblemDetailsFactory, DoubleDinnerProblemDetailsFactory>();
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}

