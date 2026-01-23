using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;

/// <summary>
/// Handler for processing CreateCustomerCommand requests.
/// </summary>
public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, CustomerResult>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateCustomerHandler.
    /// </summary>
    /// <param name="customerRepository">The customer repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public CreateCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CreateCustomerCommand request.
    /// </summary>
    /// <param name="request">The CreateCustomer command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created customer details.</returns>
    public async Task<CustomerResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateCustomerCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var existingCustomer = await _customerRepository.GetByDocumentAsync(request.Document, cancellationToken);
        if (existingCustomer != null)
            throw new InvalidOperationException($"Customer with document {request.Document} already exists");

        var customer = _mapper.Map<Customer>(request);
        var createdCustomer = await _customerRepository.CreateAsync(customer, cancellationToken);

        return _mapper.Map<CustomerResult>(createdCustomer);
    }
}