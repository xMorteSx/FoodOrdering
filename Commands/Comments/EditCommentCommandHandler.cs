using FoodOrdering.Models;
using FoodOrdering.Repositories;
using MediatR;

namespace FoodOrdering.Commands.CommentsOrderProduct;

public class EditCommentCommand : IRequest
{
    public int CommentId { get; set; }
    public string Text { get; set; }
}

public class EditCommentCommandHandler
    : IRequestHandler<EditCommentCommand>
{
    private readonly ICommentsRepository<Comment> _commentsRepository;

    public EditCommentCommandHandler(ICommentsRepository<Comment> commentsRepository)
    {
        _commentsRepository = commentsRepository;
    }

    public async Task Handle(EditCommentCommand command, CancellationToken cancellationToken)
    {
        await _commentsRepository.EditComment(command.CommentId, command.Text);
    }
}
