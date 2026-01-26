using Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.Application.Branches.GetBranch;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.CreateBranch;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches;

/// <summary>
/// Controller for managing branch operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BranchesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    /// <summary>
    /// Initializes a new instance of BranchesController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public BranchesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Creates a new branch
    /// </summary>
    /// <param name="request">The branch creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created branch details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<BranchResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] CreateBranchRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateBranchCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<BranchResponse>
        {
            Success = true,
            Message = "Branch created successfully",
            Data = _mapper.Map<BranchResponse>(response)
        });
    }
    
    /// <summary>
    /// Retrieves all branches.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of all branches.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<BranchResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllBranches(CancellationToken cancellationToken)
    {
        var command = new GetAllBranchesCommand();
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(new ApiResponseWithData<BranchResponse>
        {
            Success = true,
            Data = _mapper.Map<BranchResponse>(result)
        });
    }
}