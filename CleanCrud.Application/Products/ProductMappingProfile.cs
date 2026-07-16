using AutoMapper;
using CleanCrud.Domain.Entities;

namespace CleanCrud.Application.Products;

public sealed class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductRequest, Product>()
            .ForMember(x => x.Id, options => options.Ignore())
            .ForMember(x => x.CreatedAtUtc, options => options.Ignore());
        CreateMap<UpdateProductRequest, Product>()
            .ForMember(x => x.Id, options => options.Ignore())
            .ForMember(x => x.CreatedAtUtc, options => options.Ignore());
    }
}
