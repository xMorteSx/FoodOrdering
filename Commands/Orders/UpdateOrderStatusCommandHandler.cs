using FoodOrdering.Models;
using FoodOrdering.Repositories;
using MediatR;

namespace FoodOrdering.Commands.Orders;

public record UpdateOrderStatusCommand : IRequest
{
    public int OrderId { get; set; }
    public string Status { get; set; }
}

public class UpdateOrderStatusCommandHandler
    : IRequestHandler<UpdateOrderStatusCommand>
{
    private readonly IOrderRepository<Order> _orderRepository;

    public UpdateOrderStatusCommandHandler(IOrderRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(UpdateOrderStatusCommand command, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            Id = command.OrderId,
            Status = command.Status
        };

        await _orderRepository.UpdateOrderStatus(order.Id, order.Status);
    }
}
