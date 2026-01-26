using Ambev.DeveloperEvaluation.Application.Branches;
using Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.CreateBranch;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches;

/// <summary>
/// Profile for mapping between Application and API Branch responses
/// </summary>
public class BranchesProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for Branch feature
    /// </summary>
    public BranchesProfile()
    {
        CreateMap<CreateBranchRequest, CreateBranchCommand>();
        CreateMap<BranchResult, BranchResponse>();
    }
}