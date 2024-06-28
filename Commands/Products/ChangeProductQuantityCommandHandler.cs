using FoodOrdering.Models;
using FoodOrdering.Repositories;
using MediatR;

namespace FoodOrdering.Commands.OrderProducts;

public record ChangeProductQuantityCommand : IRequest
{
    public int Id { get; set; }
    public string Quantity { get; set; }
}

public class ChangeProductQuantityCommandHandler
    : IRequestHandler<ChangeProductQuantityCommand>
{
    private readonly IProductsRepository<Product> _orderProductRepository;

    public ChangeProductQuantityCommandHandler(IProductsRepository<Product> orderProductRepository)
    {
        _orderProductRepository = orderProductRepository;
    }

    public async Task Handle(ChangeProductQuantityCommand command, CancellationToken cancellationToken)
    {
        await _orderProductRepository.ChangeOrderProductQuantity(command.Id, command.Quantity);
    }
}
