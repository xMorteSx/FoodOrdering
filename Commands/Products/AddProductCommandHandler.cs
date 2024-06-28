using MediatR;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FoodOrdering.Repositories;
using FoodOrdering.Models;

namespace FoodOrdering.Commands.OrderProducts;

public record AddProductCommand : IRequest<Product>
{
    public int Id { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public string UserName { get; set; }

    [Required(ErrorMessage = "Nazwa produktu jest wymagana")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Kwota jest wymagana")]
    [Range(0, double.MaxValue, ErrorMessage = "Kwota nie może być mniejsza niż 0")]
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Comment { get; set; }
}

public class AddProductCommandHandler 
    : IRequestHandler<AddProductCommand, Product>
{
    private readonly IProductsRepository<Product> _orderProductsRepository;

    public AddProductCommandHandler(IProductsRepository<Product> orderProductsRepository)
    {
        _orderProductsRepository = orderProductsRepository;
    }

    public async Task<Product> Handle(AddProductCommand command, CancellationToken cancellationToken)
    {
        var orderProduct = new Product
        {
            OrderId = command.OrderId,
            UserName = command.UserName,
            Name = command.Name,
            Price = command.Price,
            Quantity = command.Quantity
        };

        await _orderProductsRepository.AddOrderProduct(orderProduct);

        return orderProduct;
    }
}
