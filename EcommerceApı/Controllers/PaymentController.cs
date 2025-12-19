using ECommerce.Services.Abstract;
using ECommerce.Services.DTOs.PaymentDto;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApı.Controllers
{
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this._paymentService = paymentService;
        }



        // bu metot yeni bir ödeme oluşturmak için kullanılır.
        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _paymentService.CreatePaymentAsync(dto);
            return StatusCode(StatusCodes.Status201Created, result);
        }



        // bu metot belirli bir ödemenin durumunu almak için kullanılır.
        [HttpGet("{paymentId}")]
        public async Task<IActionResult> GetPaymentStatus(int paymentId)
        {
            var result = await _paymentService.GetPaymentStatusAsync(paymentId);
            return Ok(result);
        }




    }
}
