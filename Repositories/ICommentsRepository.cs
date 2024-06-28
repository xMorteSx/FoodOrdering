using FoodOrdering.Data;
using FoodOrdering.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Repositories;

public interface ICommentsRepository<TEntity> where TEntity : class
{
    Task AddComment(TEntity entity);
    Task EditComment(int commentId, string text);
    Task DeleteComment(int commentId);
    Task<List<Comment>> GetComments(int orderId);
}

public class CommentsRepository<TEntity> : ICommentsRepository<TEntity> where TEntity : class
{
    private readonly DataContext _dataContext;

    public CommentsRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task AddComment(TEntity entity)
    {
        await _dataContext.AddAsync(entity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task EditComment(int commentId, string text)
    {
        (await _dataContext.Comments.FindAsync(commentId)).Text = text;
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteComment(int commentId)
    {
        var comment = await _dataContext.Comments.FindAsync(commentId);
        _dataContext.Comments.Remove(comment);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<List<Comment>> GetComments(int orderId)
    {
        var comments = await _dataContext.Comments.Where(c => c.OrderId == orderId).ToListAsync();
        return comments;
    }
}