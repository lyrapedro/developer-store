using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Customers.GetCustomer;

/// <summary>
/// Handler for processing GetAllCustomersCommand requests.
/// </summary>
public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersCommand, List<CustomerResult>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetAllCustomersHandler.
    /// </summary>
    /// <param name="customerRepository">The customer repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public GetAllCustomersHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetAllCustomersCommand request.
    /// </summary>
    /// <param name="request">The GetAllCustomers command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of all customers.</returns>
    public async Task<List<CustomerResult>> Handle(GetAllCustomersCommand request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<CustomerResult>>(customers);
    }
}