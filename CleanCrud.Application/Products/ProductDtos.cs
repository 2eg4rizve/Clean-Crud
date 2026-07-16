using System.ComponentModel.DataAnnotations;

namespace CleanCrud.Application.Products;

public sealed record ProductDto(Guid Id, string Name, string? Description, decimal Price, int StockQuantity, DateTime CreatedAtUtc);
public sealed class CreateProductRequest
{
    [Required, StringLength(200)]
    public string Name { get; init; } = string.Empty;

    [StringLength(2000)]
    public string? Description { get; init; }

    [Range(typeof(decimal), "0", "999999999.99")]
    public decimal Price { get; init; }

    [Range(0, int.MaxValue)]
    public int StockQuantity { get; init; }
}

public sealed class UpdateProductRequest
{
    [Required, StringLength(200)]
    public string Name { get; init; } = string.Empty;

    [StringLength(2000)]
    public string? Description { get; init; }

    [Range(typeof(decimal), "0", "999999999.99")]
    public decimal Price { get; init; }

    [Range(0, int.MaxValue)]
    public int StockQuantity { get; init; }
}
