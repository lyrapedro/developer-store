using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Branches.GetBranch;

public class GetBranchProfile: Profile
{
    /// <summary>
    /// Initializes the mappings for Branch operations
    /// </summary>
    public GetBranchProfile()
    {
        CreateMap<Branch, BranchResult>();
    }
}