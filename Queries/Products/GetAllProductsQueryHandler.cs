using FoodOrdering.Models;
using FoodOrdering.Models.Pagination;
using FoodOrdering.Repositories;
using MediatR;

namespace FoodOrdering.Queries.OrderProducts;

public record GetAllProductsQuery : IRequest<PaginatedList<Product>>
{
    public int OrderId { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}

public class GetAllProductsQueryHandler
    : IRequestHandler<GetAllProductsQuery, PaginatedList<Product>>
{
    private readonly IProductsRepository<PaginatedList<Product>> _orderProductsRepository;

    public GetAllProductsQueryHandler(IProductsRepository<PaginatedList<Product>> orderProductsRepository)
    {
        _orderProductsRepository = orderProductsRepository;
    }

    public async Task<PaginatedList<Product>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
    {
        return await _orderProductsRepository.GetAllOrderProductsByOrderId(query.OrderId, query.PageIndex, query.PageSize);
    }
}
