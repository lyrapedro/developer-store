using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

/// <summary>
/// Controller for managing user operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SalesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    /// <summary>
    /// Initializes a new instance of SalesController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public SalesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Creates a new sale
    /// </summary>
    /// <param name="request">The sale creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<SaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateSaleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<SaleResponse>
        {
            Success = true,
            Message = "Sale created successfully",
            Data = _mapper.Map<SaleResponse>(response)
        });
    }
    
    /// <summary>
    /// Retrieves all sales by customer.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of all sales by customer.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<SaleResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllSales(CancellationToken cancellationToken)
    {
        var command = new GetAllSalesCommand();
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(new ApiResponseWithData<SaleResponse>
        {
            Success = true,
            Data = _mapper.Map<SaleResponse>(result)
        });
    }

    /// <summary>
    /// Cancels a sale and returns stock to inventory.
    /// </summary>
    /// <param name="id">The sale ID</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <remarks>
    /// When a sale is cancelled:
    /// - The sale is marked as cancelled with timestamp and reason
    /// - Stock is returned to inventory for all items
    /// - The sale is preserved for historical/auditing purposes
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(List<CancelSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new CancelSaleCommand
        {
            Id = id
        };
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(new ApiResponseWithData<CancelSaleResponse>
        {
            Success = true,
            Data = _mapper.Map<CancelSaleResponse>(result)
        });
    }
}