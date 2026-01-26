using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Profile for mapping between Sale entity and Sale DTOs.
/// </summary>
public class GetSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for Sale-related operations.
    /// </summary>
    public GetSaleProfile()
    {
        // Sale entity -> GetSaleResult
        CreateMap<Sale, GetSaleResult>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => new GetSaleUserInfo
            {
                Id = src.UserId,
                Name = src.UserName,
                Email = src.UserEmail
            }))
            .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => new GetSaleBranchInfo
            {
                Id = src.BranchId,
                Name = src.BranchName,
                Code = src.BranchCode
            }))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        // SaleItem entity -> GetSaleItemInfo
        CreateMap<SaleItem, GetSaleItemInfo>();
    }
}