using FoodOrdering.Models;
using FoodOrdering.Repositories;
using MediatR;

namespace FoodOrdering.Commands.CommentsOrderProduct;

public record AddCommentCommand : IRequest<Comment>
{
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public string UserName { get; set; }
    public string Text { get; set; }
}

public class AddCommentCommandHandler
    : IRequestHandler<AddCommentCommand, Comment>
{
    private readonly ICommentsRepository<Comment> _commentsRepository;

    public AddCommentCommandHandler(ICommentsRepository<Comment> commentsOrderProductRepository)
    {
        _commentsRepository = commentsOrderProductRepository;
    }

    public async Task<Comment> Handle(AddCommentCommand command, CancellationToken cancellationToken)
    {
        var comment = new Comment
        {
            ProductId = command.ProductId,
            OrderId = command.OrderId,
            UserName = command.UserName,
            Text = command.Text
        };

        await _commentsRepository.AddComment(comment);

        return comment;
    }
}
