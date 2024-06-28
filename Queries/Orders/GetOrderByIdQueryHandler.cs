using FoodOrdering.Exceptions;
using FoodOrdering.Models.DTO;
using FoodOrdering.Repositories;
using MediatR;

namespace FoodOrdering.Queries.Orders;

public record GetOrderByIdQuery : IRequest<OrderDetailsDto>
{
    public int OrderId { get; set; }
}

public class GetOrderByIdQueryHandler 
    : IRequestHandler<GetOrderByIdQuery, OrderDetailsDto>
{
    private readonly IOrderRepository<OrderDetailsDto> _orderRepository;

    public GetOrderByIdQueryHandler(IOrderRepository<OrderDetailsDto> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDetailsDto> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderById(query.OrderId);

        if (order == null)
        {
            throw new OrderNotFoundException(query.OrderId);
        }

        return order;
    }
}
