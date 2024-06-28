using FoodOrdering.Models;
using FoodOrdering.Repositories;
using MediatR;

namespace FoodOrdering.Commands.CommentsOrderProduct;

public record DeleteCommentCommand : IRequest
{
    public int CommentId { get; set; }
}

public class DeleteCommentCommandHandler
    : IRequestHandler<DeleteCommentCommand>
{
    private readonly ICommentsRepository<Comment> _commentsRepository;

    public DeleteCommentCommandHandler(ICommentsRepository<Comment> commentsRepository)
    {
        _commentsRepository = commentsRepository;
    }

    public async Task Handle(DeleteCommentCommand command, CancellationToken cancellationToken)
    {
        await _commentsRepository.DeleteComment(command.CommentId);
    }
}
