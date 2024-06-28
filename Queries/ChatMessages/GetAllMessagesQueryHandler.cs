using FoodOrdering.Models;
using FoodOrdering.Repositories;
using MediatR;

namespace FoodOrdering.Queries.ChatMessages;

public record GetAllMessagesQuery : IRequest<List<ChatUserMessage>>
{
    public int OrderId { get; set; }
}

public class GetAllMessagesQueryHandler
    : IRequestHandler<GetAllMessagesQuery, List<ChatUserMessage>>
{
    private readonly IChatMessagesRepository<List<ChatUserMessage>> _chatMessagesRepository;

    public GetAllMessagesQueryHandler(IChatMessagesRepository<List<ChatUserMessage>> chatMessagesRepository)
    {
        _chatMessagesRepository = chatMessagesRepository;
    }

    public async Task<List<ChatUserMessage>> Handle(GetAllMessagesQuery query, CancellationToken cancellationToken)
    {
        return await _chatMessagesRepository.GetAllMessages(query.OrderId);
    }
}
