using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Customers.GetCustomer;

public class GetCustomerProfile: Profile
{
    /// <summary>
    /// Initializes the mappings for Customer operations
    /// </summary>
    public GetCustomerProfile()
    {
        CreateMap<Customer, CustomerResult>();
    }
}