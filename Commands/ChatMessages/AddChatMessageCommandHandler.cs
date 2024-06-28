using FoodOrdering.Models;
using FoodOrdering.Repositories;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrdering.Commands.ChatMessages;

public record AddChatMessageCommand : IRequest<Models.ChatUserMessage>
{
    public int Id { get; set; }
    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public string UserName { get; set; }
    public string Message { get; set; }
    public DateTime DateTime { get; set; }
}

public class AddChatMessageCommandHandler
    : IRequestHandler<AddChatMessageCommand, Models.ChatUserMessage>
{
    private readonly IChatMessagesRepository<Models.ChatUserMessage> _chatMessagesRepository;

    public AddChatMessageCommandHandler(IChatMessagesRepository<Models.ChatUserMessage> chatMessagesRepository)
    {
        _chatMessagesRepository = chatMessagesRepository;
    }

    public async Task<Models.ChatUserMessage> Handle(AddChatMessageCommand command, CancellationToken cancellationToken)
    {
        var chatMessage = new Models.ChatUserMessage
        {
            OrderId = command.OrderId,
            UserName = command.UserName,
            Message = command.Message,
            DateTime = command.DateTime
        };

        await _chatMessagesRepository.AddMessage(chatMessage);

        return chatMessage;
    }
}
