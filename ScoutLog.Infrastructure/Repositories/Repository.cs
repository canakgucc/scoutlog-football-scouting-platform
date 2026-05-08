using Microsoft.EntityFrameworkCore;
using ScoutLog.Application.Interfaces.Repositories;
using ScoutLog.Infrastructure.Persistence;

namespace ScoutLog.Infrastructure.Repositories;

public class Repository<TEntity>(ScoutLogDbContext context) : IRepository<TEntity>
    where TEntity : class
{
    public IQueryable<TEntity> Query()
    {
        return context.Set<TEntity>().AsQueryable();
    }

    public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Set<TEntity>().FindAsync([id], cancellationToken);
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<TEntity>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await context.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public void Update(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(cancellationToken);
    }
}
