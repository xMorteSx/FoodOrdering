using FoodOrdering.Data;
using FoodOrdering.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Repositories;

public interface IChatMessagesRepository<TEntity> where TEntity : class
{
    Task AddMessage(ChatUserMessage message);
    Task<List<ChatUserMessage>> GetAllMessages(int orderId);
}

public class ChatMessagesRepository<TEntity> : IChatMessagesRepository<TEntity> where TEntity : class
{
    private readonly DataContext _dataContext;

    public ChatMessagesRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task AddMessage(ChatUserMessage message)
    {
        await _dataContext.ChatMessages.AddAsync(message);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<List<ChatUserMessage>> GetAllMessages(int orderId)
    {
        var messages = await _dataContext.ChatMessages
            .Where(m => m.OrderId == orderId)
            .ToListAsync();
        return messages;
    }
}
