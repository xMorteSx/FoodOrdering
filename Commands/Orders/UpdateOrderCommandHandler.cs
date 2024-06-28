using FoodOrdering.Models.DTO;
using FoodOrdering.Repositories;
using MediatR;

namespace FoodOrdering.Commands.Orders;

public class UpdateOrderCommand : IRequest
{
    public int Id { get; set; }
    public string PhoneNumber { get; set; }
    public string BankAccountNumber { get; set; }
    public string RestaurantName { get; set; }
    public decimal MinPrice { get; set; }
    public decimal DeliveryPrice { get; set; }
    public decimal FreeDeliveryPrice { get; set; }
}

public class UpdateOrderCommandHandler
    : IRequestHandler<UpdateOrderCommand>
{
    private readonly IOrderRepository<OrderUpdateDto> _orderRepository;

    public UpdateOrderCommandHandler(IOrderRepository<OrderUpdateDto> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = new OrderUpdateDto
        {
            Id = command.Id,
            PhoneNumber = command.PhoneNumber,
            BankAccountNumber = command.BankAccountNumber,
            RestaurantName = command.RestaurantName,
            MinPrice = command.MinPrice,
            DeliveryPrice = command.DeliveryPrice,
            FreeDeliveryPrice = command.FreeDeliveryPrice,
        };

        await _orderRepository.UpdateOrder(order);
    }
}
