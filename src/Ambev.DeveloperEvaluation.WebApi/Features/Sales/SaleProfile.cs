using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

public class SaleProfile : Profile
{
    public SaleProfile()
    {
        CreateMap<UserInfo, GetSaleCustomerInfo>();
        CreateMap<BranchInfo, GetSaleBranchInfo>();
        CreateMap<SaleItemInfo,  GetSaleItemInfo>();
        CreateMap<GetSaleUserInfo, GetSaleCustomerInfo>();
        CreateMap<Application.Sales.GetSale.GetSaleBranchInfo, GetSaleBranchInfo>();
        CreateMap<Application.Sales.GetSale.GetSaleItemInfo,  GetSaleItemInfo>();
        CreateMap<CreateSaleItemRequest, CreateSaleItemCommand>();
        CreateMap<CreateSaleRequest, CreateSaleCommand>();
        CreateMap<GetSaleResult, SaleResponse>();
        CreateMap<CreateSaleResult, SaleResponse>();
        CreateMap<CancelSaleResult, CancelSaleResponse>();
    }
}