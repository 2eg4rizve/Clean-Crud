using CleanCrud.Application.Contracts;
using CleanCrud.Domain.Entities;
using CleanCrud.Infrastructure.Repositories;

namespace CleanCrud.Infrastructure.Persistence;

public sealed class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
    private IGenericRepository<Product>? _products;

    public IGenericRepository<Product> Products => _products ??= new GenericRepository<Product>(dbContext);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        dbContext.SaveChangesAsync(cancellationToken);
}
