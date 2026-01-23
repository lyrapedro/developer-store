using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Branches;

/// <summary>
/// Profile for mapping between Branch entity and Branch DTOs.
/// </summary>
public class BranchProfile : Profile
{
    public BranchProfile()
    {
        // CreateBranchCommand -> Branch entity
        CreateMap<CreateBranchCommand, Branch>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        // Branch entity -> CreateBranchResult
        CreateMap<Branch, BranchResult>();
    }
}