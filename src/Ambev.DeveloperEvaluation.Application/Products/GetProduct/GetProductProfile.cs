using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

public class GetProductProfile: Profile
{
    /// <summary>
    /// Initializes the mappings for Product operations
    /// </summary>
    public GetProductProfile()
    {
        CreateMap<Product, ProductResult>();
    }
}