using FoodOrdering.Commands.ChatMessages;
using FoodOrdering.Queries.ChatMessages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChatMessagesController(IMediator mediatR)
        {
            _mediator = mediatR;
        }

        [HttpPost("addMessage")]
        public async Task<IActionResult> AddMessage(AddChatMessageCommand command)
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

        [HttpGet("getMessages/{orderId}")]
        public async Task<IActionResult> GetAllMessages(int orderId)
        {
            try
            {
                return Ok(await _mediator.Send(new GetAllMessagesQuery() { OrderId = orderId }));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
