using FoodOrdering.Models;
using FoodOrdering.Repositories;
using MediatR;

namespace FoodOrdering.Commands.OrderProducts;

public record DeleteProductCommand : IRequest
{
    public int OrderProductId { get; set; }
}

public class DeleteProductCommandHandler
    : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductsRepository<Product> _orderProductsRepository;

    public DeleteProductCommandHandler(IProductsRepository<Product> orderProductsRepository)
    {
        _orderProductsRepository = orderProductsRepository;
    }

    public async Task Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        await _orderProductsRepository.DeleteOrderProduct(command.OrderProductId);
    }
}
