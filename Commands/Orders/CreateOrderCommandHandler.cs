using FoodOrdering.Models;
using FoodOrdering.Repositories;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Commands.Orders;

public record CreateOrderCommand : IRequest
{
    public int Id { get; set; }
    public string UserEmail { get; set; }
    public string Status { get; set; }

    [Required(ErrorMessage = "Nazwa zamówienia jest wymagana")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Numer telefonu jest wymagany")]
    [Phone(ErrorMessage = "Proszę podać numer komórkowy (9 cyfr)")]
    [RegularExpression(@"^\d{9}$", ErrorMessage = "Proszę podać numer komórkowy (9 cyfr)")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Konto bankowe jest wymagane")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Konto bankowe może zawierać tylko cyfry")]
    public string BankAccountNumber { get; set; }

    [Required(ErrorMessage = "Nazwa restauracji jest wymagana")]
    public string RestaurantName { get; set; }

    [Required(ErrorMessage = "Minimalna kwota jest wymagana")]
    [Range(0, double.MaxValue, ErrorMessage = "Kwota nie może być mniejsza niż 0")]
    public decimal MinPrice { get; set; }

    [Required(ErrorMessage = "Koszt dostawy jest wymagany")]
    [Range(0, double.MaxValue, ErrorMessage = "Kwota nie może być mniejsza niż 0")]
    public decimal DeliveryPrice { get; set; }

    [Required(ErrorMessage = "Koszt darmowej dostawy jest wymagany")]
    [Range(0, double.MaxValue, ErrorMessage = "Kwota nie może być mniejsza niż 0")]
    public decimal FreeDeliveryPrice { get; set; }
    public DateTime Date { get; set; }
}

public class CreateOrderCommandHandler
    : IRequestHandler<CreateOrderCommand>
{
    private readonly IOrderRepository<Order> _orderRepository;

    public CreateOrderCommandHandler(IOrderRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            UserEmail = command.UserEmail,
            Status = command.Status,
            Name = command.Name,
            PhoneNumber = command.PhoneNumber,
            BankAccountNumber = command.BankAccountNumber,
            RestaurantName = command.RestaurantName,
            MinPrice = command.MinPrice,
            DeliveryPrice = command.DeliveryPrice,
            FreeDeliveryPrice = command.FreeDeliveryPrice,
            Date = command.Date
        };

        await _orderRepository.CreateOrder(order);
    }
}

