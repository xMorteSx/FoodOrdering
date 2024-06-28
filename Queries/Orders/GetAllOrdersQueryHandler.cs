using FoodOrdering.Models.DTO;
using FoodOrdering.Models.Pagination;
using FoodOrdering.Repositories;
using MediatR;

namespace FoodOrdering.Queries.Orders;

public record GetAllOrdersQuery : IRequest<PaginatedList<OrderDto>>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string SelectedFilters { get; set; }
}

public class GetAllOrdersQueryHandler
    : IRequestHandler<GetAllOrdersQuery, PaginatedList<OrderDto>>
{
    private readonly IOrderRepository<PaginatedList<OrderDto>> _orderRepository;

    public GetAllOrdersQueryHandler(IOrderRepository<PaginatedList<OrderDto>> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<PaginatedList<OrderDto>> Handle(GetAllOrdersQuery query, CancellationToken cancellationToken)
    {
        return await _orderRepository.GetAllOrders(query.PageIndex, query.PageSize, query.SelectedFilters);
    }
}
