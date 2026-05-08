using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ScoutLog.Infrastructure.Persistence;

public class ScoutLogDbContextFactory : IDesignTimeDbContextFactory<ScoutLogDbContext>
{
    public ScoutLogDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("SCOUTLOG_CONNECTION_STRING")
            ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("A design-time SQL connection string is required. Set SCOUTLOG_CONNECTION_STRING or ConnectionStrings__DefaultConnection before running EF commands.");
        }

        var optionsBuilder = new DbContextOptionsBuilder<ScoutLogDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new ScoutLogDbContext(optionsBuilder.Options);
    }
}
