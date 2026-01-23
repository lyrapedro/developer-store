using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Command for retrieving all sales.
/// </summary>
public class GetAllSalesCommand : IRequest<List<GetSaleResult>>
{
}