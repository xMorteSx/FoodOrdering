using FoodOrdering.Models;
using FoodOrdering.Repositories;
using FoodOrdering.Services;
using MediatR;

namespace FoodOrdering.Commands.Orders;

public record DeleteOrderCommand : IRequest
{
    public int Id { get; set; }
    public string UserMail { get; set; }
}

public class DeleteOrderCommandHandler
    : IRequestHandler<DeleteOrderCommand>
{
    private readonly IOrderRepository<Order> _orderRepository;
    private readonly GraphService _graphService;

    public DeleteOrderCommandHandler(IOrderRepository<Order> orderRepository, GraphService graphService)
    {
        _orderRepository = orderRepository;
        _graphService = graphService;
    }

    public async Task Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        /*var currentUser = await _graphService.GetCurrentUserProfileAsync();
        var order = await _orderRepository.GetOrderById(command.Id);

        if (currentUser.Mail == order.UserEmail)
        {
            await _orderRepository.DeleteOrder(command.Id);
        }*/

        var order = await _orderRepository.GetOrderById(command.Id);
        
        if (order.UserEmail == command.UserMail)
        {
            await _orderRepository.DeleteOrder(command.Id);
        }
    }
}
