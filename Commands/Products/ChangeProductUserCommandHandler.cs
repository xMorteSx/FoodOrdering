using FoodOrdering.Models;
using FoodOrdering.Repositories;
using MediatR;

namespace FoodOrdering.Commands.OrderProducts;

public record ChangeProductUserCommand : IRequest
{
    public int OrderProductId { get; set; }
    public string UserName { get; set; }
}

public class ChangeProductUserCommandHandler
    : IRequestHandler<ChangeProductUserCommand>
{
    private readonly IProductsRepository<Product> _orderProductsRepository;

    public ChangeProductUserCommandHandler(IProductsRepository<Product> orderProductsRepository)
    {
        _orderProductsRepository = orderProductsRepository;
    }

    public async Task Handle(ChangeProductUserCommand command, CancellationToken cancellationToken)
    {
        await _orderProductsRepository.ChangeProductUser(command.OrderProductId, command.UserName);
    }
}
