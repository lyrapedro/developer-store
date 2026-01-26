using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;
using Ambev.DeveloperEvaluation.Application.Customers.GetCustomer;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Customers.CreateCustomer;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers;

/// <summary>
/// Controller for managing customer operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CustomersController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    /// <summary>
    /// Initializes a new instance of CustomersController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CustomersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Creates a new customer
    /// </summary>
    /// <param name="request">The customer creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created customer details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CustomerResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateCustomerCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CustomerResponse>
        {
            Success = true,
            Message = "Customer created successfully",
            Data = _mapper.Map<CustomerResponse>(response)
        });
    }
    
    /// <summary>
    /// Retrieves all customers.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of all customers.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<CustomerResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCustomers(CancellationToken cancellationToken)
    {
        var command = new GetAllCustomersCommand();
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(new ApiResponseWithData<CustomerResponse>
        {
            Success = true,
            Data = _mapper.Map<CustomerResponse>(result)
        });
    }
}