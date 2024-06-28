using FoodOrdering.Data;
using FoodOrdering.Models;
using FoodOrdering.Models.DTO;
using FoodOrdering.Models.Pagination;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Repositories;

public interface IOrderRepository<TEntity> where TEntity : class
{
    Task CreateOrder(TEntity entity);
    Task UpdateOrder(OrderUpdateDto order);
    Task UpdateOrderStatus(int orderId, string orderStatus);
    Task DeleteOrder(int orderId);

    Task<PaginatedList<OrderDto>> GetAllOrders(int pageIndex, int pageSize, string selectedFilters);
    Task<OrderDetailsDto> GetOrderById(int id);
}

public class OrderRepository<TEntity> : IOrderRepository<TEntity> where TEntity : class
{
    private readonly DataContext _dataContext;

    public OrderRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task CreateOrder(TEntity entity)
    {
        await _dataContext.Set<TEntity>().AddAsync(entity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task UpdateOrder(OrderUpdateDto order)
    {
        var updatedOrder = await _dataContext.Orders.FindAsync(order.Id);

        updatedOrder.PhoneNumber = order.PhoneNumber;
        updatedOrder.BankAccountNumber = order.BankAccountNumber;
        updatedOrder.RestaurantName = order.RestaurantName;
        updatedOrder.MinPrice = order.MinPrice;
        updatedOrder.DeliveryPrice = order.DeliveryPrice;
        updatedOrder.FreeDeliveryPrice = order.FreeDeliveryPrice;

        await _dataContext.SaveChangesAsync();
    }

    public async Task UpdateOrderStatus(int orderId, string status)
    {
        (await _dataContext.Orders.FindAsync(orderId)).Status = status;
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteOrder(int orderId)
    {
        var order = await _dataContext.Orders.FindAsync(orderId);
        _dataContext.Orders.Remove(order);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<PaginatedList<OrderDto>> GetAllOrders(int pageIndex, int pageSize, string selectedFilters)
    {
        var filters = !string.IsNullOrEmpty(selectedFilters) ? selectedFilters.Split(',') : Array.Empty<string>();
        var orders = _dataContext.Orders
            .Where(o => filters.Contains(o.Status))
            .Select(o => new OrderDto
            {
                Id = o.Id,
                Name = o.Name,
                UserEmail = o.UserEmail,
                Status = o.Status,
                Date = o.Date
            });

        var count = await orders.CountAsync();
        var items = await orders.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        var totalPages = (int)Math.Ceiling(count / (double)pageSize);

        return new PaginatedList<OrderDto>(items, pageIndex, totalPages);
    }

    public Task<OrderDetailsDto> GetOrderById(int id)
    {
        var order = _dataContext.Orders
            .Where(o => o.Id == id)
            .Select(o => new OrderDetailsDto
            {
                Id = o.Id,
                Name = o.Name,
                UserEmail = o.UserEmail,
                Status = o.Status,
                PhoneNumber = o.PhoneNumber,
                BankAccountNumber = o.BankAccountNumber,
                RestaurantName = o.RestaurantName,
                MinPrice = o.MinPrice,
                DeliveryPrice = o.DeliveryPrice,
                FreeDeliveryPrice = o.FreeDeliveryPrice
            })
            .FirstOrDefaultAsync();


        return order;
    }
}
