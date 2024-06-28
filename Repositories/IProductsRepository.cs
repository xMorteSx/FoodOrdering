using FoodOrdering.Data;
using FoodOrdering.Models;
using FoodOrdering.Models.Pagination;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Repositories;

public interface IProductsRepository<TEntity> where TEntity : class
{
    Task AddOrderProduct(TEntity entity);
    Task ChangeProductUser(int orderProductId, string userName);
    Task ChangeOrderProductQuantity(int orderProductId, string quantity);
    Task DeleteOrderProduct(int orderProductId);
    Task<PaginatedList<Product>> GetAllOrderProductsByOrderId(int orderId, int pageIndex, int pageSize);
}

public class ProductsRepository<TEntity> : IProductsRepository<TEntity> where TEntity : class
{
    private readonly DataContext _dataContext;

    public ProductsRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task AddOrderProduct(TEntity entity)
    {
        await _dataContext.Set<TEntity>().AddAsync(entity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task ChangeProductUser(int orderProductId, string userName)
    {
        (await _dataContext.Products.FindAsync(orderProductId)).UserName = userName;
        await _dataContext.SaveChangesAsync();
    }

    public async Task ChangeOrderProductQuantity(int orderProductId, string quantity)
    {
        switch (quantity)
        {
            case "Increase":
                (await _dataContext.Products.FindAsync(orderProductId)).Quantity++;
                await _dataContext.SaveChangesAsync();
                break;
            case "Decrease":
                (await _dataContext.Products.FindAsync(orderProductId)).Quantity--;
                await _dataContext.SaveChangesAsync();
                break;
        }
    }

    public async Task DeleteOrderProduct(int orderProductId)
    {
        var orderProduct = await _dataContext.Products.FindAsync(orderProductId);

        _dataContext.Products.Remove(orderProduct);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<PaginatedList<Product>> GetAllOrderProductsByOrderId(int orderId, int pageIndex, int pageSize)
    {
        var orderProducts = _dataContext.Products
            .Include(op => op.Comments)
            .Where(op => op.OrderId == orderId);

        var count = await orderProducts.CountAsync();
        var items = await orderProducts.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        var totalPages = (int)Math.Ceiling(count / (double)pageSize);

        return new PaginatedList<Product>(items, pageIndex, totalPages);
    }
}