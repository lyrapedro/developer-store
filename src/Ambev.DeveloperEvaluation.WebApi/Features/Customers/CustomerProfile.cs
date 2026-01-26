using Ambev.DeveloperEvaluation.Application.Customers;
using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;
using Ambev.DeveloperEvaluation.WebApi.Features.Customers.CreateCustomer;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers;

/// <summary>
/// Profile for mapping between Application and API Customer responses
/// </summary>
public class CustomerProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for Customer feature
    /// </summary>
    public CustomerProfile()
    {
        CreateMap<CreateCustomerRequest, CreateCustomerCommand>();
        CreateMap<CustomerResult, CustomerResponse>();
    }
}