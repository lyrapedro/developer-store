using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Branches.GetBranch;

/// <summary>
/// Handler for processing GetAllBranchesCommand requests.
/// </summary>
public class GetAllBranchesHandler : IRequestHandler<GetAllBranchesCommand, List<BranchResult>>
{
    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetAllBranchesHandler.
    /// </summary>
    /// <param name="branchRepository">The branch repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public GetAllBranchesHandler(IBranchRepository branchRepository, IMapper mapper)
    {
        _branchRepository = branchRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetAllBranchesCommand request.
    /// </summary>
    /// <param name="request">The GetAllBranches command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of all branches.</returns>
    public async Task<List<BranchResult>> Handle(GetAllBranchesCommand request, CancellationToken cancellationToken)
    {
        var branches = await _branchRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<BranchResult>>(branches);
    }
}