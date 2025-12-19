using ECommerce.Services.Abstract;
using ECommerce.Services.DTOs.ShippingDto;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApı.Controllers
{
    public class ShippingController : ControllerBase
    {


        private readonly IShippingService _shippingService;

        public ShippingController(IShippingService shippingService)
        {
            _shippingService = shippingService;
        }

     




        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var shippings = await _shippingService.GetAllAsync();
            return Ok(shippings);
        }

     
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var shipping = await _shippingService.GetByIdAsync(id);
            return Ok(shipping);
        }

       
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ShippingCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _shippingService.CreateAsync(dto);
            return StatusCode(StatusCodes.Status201Created);
        }

       
        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatus([FromBody] ShippingUpdateStatusDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _shippingService.UpdateStatusAsync(dto);
            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _shippingService.DeleteAsync(id);
            return NoContent();
        }
    }
}
