using ECommerce.Services.Abstract;
using ECommerce.Services.DTOs.OrderDto;
using ECommerce.Services.DTOs.ShippingDto;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EcommerceApı.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IShippingService _shippingService;

        public OrderController(IOrderService orderService, IShippingService shippingService)
        {
            _orderService = orderService;
            _shippingService = shippingService;
        }




        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrder)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 1️⃣ Siparişi oluştur
            var order = await _orderService.CreateOrderAsync(createOrder);

            // 2️⃣ Otomatik kargo kaydı oluştur
            var shippingDto = new ShippingCreateDto
            {
                OrderId = order.Id,
                Address = createOrder.Address,
                ShippingType = "Standard",
                Status = "Hazırlanıyor",
                EstimatedDelivery = DateTime.UtcNow.AddDays(3)
            };

            await _shippingService.CreateAsync(shippingDto);

            return StatusCode(StatusCodes.Status201Created, order);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var order = await _orderService.GetOrderDetailAsync(id);
            return Ok(order);
        }


        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUser(int userId)
        {
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }
    }
}
