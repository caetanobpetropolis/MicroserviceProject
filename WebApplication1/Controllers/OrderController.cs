using Microsoft.AspNetCore.Mvc;
using OrderService.Services;
using WebApplication1.Models;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly SqsService _sqsService;

        public OrderController(SqsService sqsService)
        {
            _sqsService = sqsService;
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            order.Id = Guid.NewGuid();
            await _sqsService.SendMessageAsync(order);
            return Ok(new { message = "Order sent to queue", orderId = order.Id });
        }
    }
}
