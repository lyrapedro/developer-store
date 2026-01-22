using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;

public class CreateBranchProfile: Profile
{
    /// <summary>
    /// Initializes the mappings for Branch operations
    /// </summary>
    public CreateBranchProfile()
    {
        CreateMap<CreateBranchCommand, Branch>();
        CreateMap<Branch, BranchResult>();
    }
}