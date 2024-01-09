using Domain.Repositories;
using Infrastructure.ExternalServices.RandomTeamMemberApiService;
using Infrastructure.Presistence;
using Infrastructure.Presistence.Repositories;
using Infrastructure.Shared.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfractucture(this IServiceCollection services, 
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresConnection");

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<ApplicationDbContext>();

        services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();

        services.AddTransient<IRandomTeamMemberApiService, RandomTeamMemberApiSerivce>();

        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}