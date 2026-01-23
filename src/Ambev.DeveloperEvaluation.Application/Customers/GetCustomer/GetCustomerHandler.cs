using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Customers.GetCustomer;

/// <summary>
/// Handler for processing GetCustomerCommand requests.
/// </summary>
public class GetCustomerHandler : IRequestHandler<GetCustomerCommand, CustomerResult>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetCustomerHandler.
    /// </summary>
    /// <param name="customerRepository">The customer repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public GetCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetCustomerCommand request.
    /// </summary>
    /// <param name="request">The GetCustomer command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The customer details if found.</returns>
    public async Task<CustomerResult> Handle(GetCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (customer == null)
            throw new KeyNotFoundException($"Customer with ID {request.Id} not found");

        return _mapper.Map<CustomerResult>(customer);
    }
}