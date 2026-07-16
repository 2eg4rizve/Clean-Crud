using CleanCrud.Application.Contracts;
using CleanCrud.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanCrud.Infrastructure.Repositories;

public sealed class GenericRepository<TEntity>(ApplicationDbContext dbContext) : IGenericRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _entities = dbContext.Set<TEntity>();

    public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _entities.AsNoTracking().ToListAsync(cancellationToken);

    public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _entities.FindAsync([id], cancellationToken).AsTask();

    public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default) =>
        _entities.AddAsync(entity, cancellationToken).AsTask();

    public void Update(TEntity entity) => _entities.Update(entity);

    public void Delete(TEntity entity) => _entities.Remove(entity);
}
