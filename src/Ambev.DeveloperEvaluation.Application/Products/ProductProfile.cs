using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products;

/// <summary>
/// Profile for mapping between Product entity and Product DTOs.
/// </summary>
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        // CreateProductCommand -> Product entity
        CreateMap<CreateProductCommand, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        // Product entity -> CreateProductResult
        CreateMap<Product, ProductResult>();
    }
}