using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Command for retrieving all products.
/// </summary>
public class GetAllProductsCommand : IRequest<List<ProductResult>>
{
}