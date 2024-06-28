using FoodOrdering.Commands.OrderProducts;
using FoodOrdering.Queries.OrderProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderProduct(AddProductCommand command)
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

        [HttpPost("change/{orderProductId}")]
        public async Task<IActionResult> ChangeProductUser(int orderProductId, [FromBody] string userName)
        {
            try
            {
                await _mediator.Send(new ChangeProductUserCommand()
                { OrderProductId = orderProductId, UserName = userName });
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("changeQuantity/{orderProductId}")]
        public async Task<IActionResult> ChangeOrderProductQuantity(int orderProductId, [FromBody] string quantity)
        {
            try
            {
                await _mediator.Send(new ChangeProductQuantityCommand() { Id = orderProductId, Quantity = quantity });
                return Ok();
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message); 
            }
        }

        [HttpPost("delete/{orderProductId}")]
        public async Task<IActionResult> DeleteOrderProduct(int orderProductId)
        {
            try
            {
                await _mediator.Send(new DeleteProductCommand() { OrderProductId = orderProductId });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getProducts")]
        public async Task<IActionResult> GetAllProducts([FromQuery] int orderId, [FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            try
            {
                var orderProducts = await _mediator.Send(new GetAllProductsQuery()
                { OrderId = orderId, PageIndex = pageIndex, PageSize = pageSize });
                return Ok(orderProducts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
