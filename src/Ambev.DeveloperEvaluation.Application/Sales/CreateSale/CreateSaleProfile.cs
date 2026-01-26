using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Profile for mapping between Sale entity and CreateSaleResponse
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSale operation
    /// </summary>
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleCommand, Sale>();
        
        // Sale entity -> CreateSaleResult
        CreateMap<Sale, CreateSaleResult>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => new UserInfo
            {
                Id = src.UserId,
                Name = src.UserName,
                Email = src.UserEmail
            }))
            .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => new BranchInfo
            {
                Id = src.BranchId,
                Name = src.BranchName,
                Code = src.BranchCode
            }))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        // SaleItem entity -> SaleItemInfo
        CreateMap<SaleItem, SaleItemInfo>();

        // CreateSaleItemCommand -> SaleItem entity
        CreateMap<CreateSaleItemCommand, SaleItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.SaleId, opt => opt.Ignore())
            .ForMember(dest => dest.ProductName, opt => opt.Ignore())
            .ForMember(dest => dest.ProductSku, opt => opt.Ignore())
            .ForMember(dest => dest.TotalAmount, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
    }
}