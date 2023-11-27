using LogManager.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LogManager.Data.Repositories;

/// <summary>
/// Generic repository class for performing common database operations.
/// </summary>
/// <typeparam name="TEntity">The entity type to operate on.</typeparam>
public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly LogDbContext LogDbContext;

    public Repository(LogDbContext logDbContext)
    {
        LogDbContext = logDbContext;
    }

    /// <inheritdoc cref="IRepository{TEntity}.GetByIdAsync(int, CancellationToken)"/>
    public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await LogDbContext.Set<TEntity>().FindAsync(id, cancellationToken);
    }
    /// <inheritdoc cref="IRepository{TEntity}.GetAllAsync(CancellationToken)"/>
    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await LogDbContext.Set<TEntity>().ToListAsync(cancellationToken);
    }
    /// <inheritdoc cref="IRepository{TEntity}.AddAsync(TEntity, CancellationToken)"/>
    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await LogDbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        return entity;
    }
    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return LogDbContext.Set<TEntity>().Where(predicate);
    }
    public virtual async Task<bool> CommitChangeAsync( CancellationToken cancellationToken)
    {
        await LogDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}

