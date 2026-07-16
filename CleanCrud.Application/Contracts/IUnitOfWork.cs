using CleanCrud.Domain.Entities;

namespace CleanCrud.Application.Contracts;

public interface IUnitOfWork
{
    IGenericRepository<Product> Products { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
