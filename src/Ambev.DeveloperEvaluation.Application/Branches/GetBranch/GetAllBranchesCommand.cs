using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branches.GetBranch;

/// <summary>
/// Command for retrieving all branches.
/// </summary>
public class GetAllBranchesCommand : IRequest<List<BranchResult>>
{
}