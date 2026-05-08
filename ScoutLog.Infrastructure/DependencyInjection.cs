using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScoutLog.Application.Interfaces.Repositories;
using ScoutLog.Application.Interfaces.Security;
using ScoutLog.Infrastructure.Persistence;
using ScoutLog.Infrastructure.Repositories;
using ScoutLog.Infrastructure.Security;

namespace ScoutLog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("ConnectionStrings:DefaultConnection is not configured. Set it with user-secrets or the ConnectionStrings__DefaultConnection environment variable.");
        }

        services.AddDbContext<ScoutLogDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<IScoutReportRepository, ScoutReportRepository>();
        services.AddScoped<IPerformanceMetricRepository, PerformanceMetricRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, Sha256PasswordHasher>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}
