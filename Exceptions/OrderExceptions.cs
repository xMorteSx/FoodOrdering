namespace FoodOrdering.Exceptions;

public class OrderNotFoundException : Exception
{
    public OrderNotFoundException(int? orderId)
        : base($"Zamówienie o identyfikatorze - '{orderId}' nie znaleziono.")
    {
    }
}
