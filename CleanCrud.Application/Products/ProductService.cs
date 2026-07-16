using AutoMapper;
using CleanCrud.Application.Contracts;
using CleanCrud.Domain.Entities;

namespace CleanCrud.Application.Products;

public sealed class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
{
    public async Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default) =>
        mapper.Map<List<ProductDto>>(await unitOfWork.Products.GetAllAsync(cancellationToken));

    public async Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await unitOfWork.Products.GetByIdAsync(id, cancellationToken);
        return product is null ? null : mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> CreateAsync(CreateProductRequest request, CancellationToken cancellationToken = default)
    {
        var product = mapper.Map<Product>(request);
        Normalize(product);
        await unitOfWork.Products.AddAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto?> UpdateAsync(Guid id, UpdateProductRequest request, CancellationToken cancellationToken = default)
    {
        var product = await unitOfWork.Products.GetByIdAsync(id, cancellationToken);
        if (product is null) return null;
        mapper.Map(request, product);
        Normalize(product);
        unitOfWork.Products.Update(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<ProductDto>(product);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await unitOfWork.Products.GetByIdAsync(id, cancellationToken);
        if (product is null) return false;
        unitOfWork.Products.Delete(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static void Normalize(Product product)
    {
        product.Name = product.Name.Trim();
        product.Description = product.Description?.Trim();
    }
}
