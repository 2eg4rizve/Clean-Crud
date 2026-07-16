using CleanCrud.Application.Contracts;
using CleanCrud.Domain.Entities;

namespace CleanCrud.Application.Products;

public sealed class ProductService(IProductRepository repository) : IProductService
{
    public async Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default) =>
        (await repository.GetAllAsync(cancellationToken)).Select(ToDto).ToList();

    public async Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await repository.GetByIdAsync(id, cancellationToken);
        return product is null ? null : ToDto(product);
    }

    public async Task<ProductDto> CreateAsync(CreateProductRequest request, CancellationToken cancellationToken = default)
    {
        Validate(request.Name, request.Price, request.StockQuantity);
        var product = new Product { Name = request.Name.Trim(), Description = request.Description?.Trim(), Price = request.Price, StockQuantity = request.StockQuantity };
        await repository.AddAsync(product, cancellationToken);
        return ToDto(product);
    }

    public async Task<ProductDto?> UpdateAsync(Guid id, UpdateProductRequest request, CancellationToken cancellationToken = default)
    {
        Validate(request.Name, request.Price, request.StockQuantity);
        var product = await repository.GetByIdAsync(id, cancellationToken);
        if (product is null) return null;
        product.Name = request.Name.Trim();
        product.Description = request.Description?.Trim();
        product.Price = request.Price;
        product.StockQuantity = request.StockQuantity;
        await repository.UpdateAsync(product, cancellationToken);
        return ToDto(product);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await repository.GetByIdAsync(id, cancellationToken);
        if (product is null) return false;
        await repository.DeleteAsync(product, cancellationToken);
        return true;
    }

    private static void Validate(string name, decimal price, int stockQuantity)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");
        if (price < 0) throw new ArgumentException("Price cannot be negative.");
        if (stockQuantity < 0) throw new ArgumentException("Stock quantity cannot be negative.");
    }

    private static ProductDto ToDto(Product product) => new(product.Id, product.Name, product.Description, product.Price, product.StockQuantity, product.CreatedAtUtc);
}
