using FoodOrdering.Commands.Orders;
using FoodOrdering.Exceptions;
using FoodOrdering.Models.DTO;
using FoodOrdering.Queries.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace FoodOrdering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("updateOrder")]
        public async Task<IActionResult> UpdateOrder(OrderUpdateDto order)
        {
            try
            {
                await _mediator.Send(new UpdateOrderCommand()
                {
                    Id = order.Id,
                    PhoneNumber = order.PhoneNumber,
                    BankAccountNumber = order.BankAccountNumber,
                    RestaurantName = order.RestaurantName,
                    MinPrice = order.MinPrice,
                    DeliveryPrice = order.DeliveryPrice,
                    FreeDeliveryPrice = order.FreeDeliveryPrice
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{orderId}")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] string orderStatus)
        {
            try
            {
                await _mediator.Send(new UpdateOrderStatusCommand { OrderId = orderId, Status = orderStatus });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("delete/{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId, [FromBody] string userMail)
        {
            try
            {
                await _mediator.Send(new DeleteOrderCommand() { Id = orderId, UserMail = userMail });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getAllOrders")]
        public async Task<IActionResult> GetAllOrders([FromQuery] string selectedFilters, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var orders = await _mediator.Send(new GetAllOrdersQuery() { PageIndex = pageIndex, PageSize = pageSize, SelectedFilters = selectedFilters });
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getOrderById/{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            try
            {
                var order = await _mediator.Send(new GetOrderByIdQuery() { OrderId = orderId });
                return Ok(order);
            }
            catch (OrderNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}
