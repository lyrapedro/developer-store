using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.GetCustomer;

/// <summary>
/// Command for retrieving all customers.
/// </summary>
public class GetAllCustomersCommand : IRequest<List<CustomerResult>>
{
}