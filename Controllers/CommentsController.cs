using FoodOrdering.Commands.CommentsOrderProduct;
using FoodOrdering.Queries.ChatMessages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("addComment")]
        public async Task<IActionResult> AddComment(AddCommentCommand command)
        {
            try
            {
                return Ok(await _mediator.Send(command));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("editComment/{commentId}")]
        public async Task<IActionResult> EditComment(int commentId, [FromBody] string text)
        {
            try
            {
                await _mediator.Send(new EditCommentCommand() { CommentId = commentId, Text = text });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteComment/{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            try
            {
                await _mediator.Send(new DeleteCommentCommand() { CommentId = commentId });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getComments/{orderId}")]
        public async Task<IActionResult> GetComments(int orderId)
        {
            try
            {
                return Ok(await _mediator.Send(new GetAllMessagesQuery() { OrderId = orderId }));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
