using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Customers;

/// <summary>
/// Profile for mapping between Customer entity and Customer DTOs.
/// </summary>
public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        // CreateCustomerCommand -> Customer entity
        CreateMap<CreateCustomerCommand, Customer>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore());

        // Customer entity -> CustomerResult
        CreateMap<Customer, CustomerResult>();
    }
}