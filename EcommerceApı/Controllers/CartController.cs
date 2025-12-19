using ECommerce.Services.Abstract;
using ECommerce.Services.DTOs.CartDtos;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApı.Controllers
{
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            this._cartService = cartService;
        }



        // GET: api/cart/{userId}
        // Kullanıcının sepetini getir
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var cart = await _cartService.GetCartAsync(userId);
            return Ok(cart);
        }



        // POST: api/cart/{userId}
        // Sepete ürün ekle
        [HttpPost("{userId}")]
        public async Task<IActionResult> AddToCart(int userId, [FromBody] AddToCartDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _cartService.AddToCartAsync(userId, dto);
            return Ok("Ürün sepete eklendi");
        }



        // DELETE: api/cart/{userId}/product/{productId}
        // Sepetten ürün sil
        [HttpDelete("{userId}/product/{productId}")]
        public async Task<IActionResult> RemoveFromCart(int userId, int productId)
        {
            await _cartService.RemoveFromCartAsync(userId, productId);
            return NoContent();
        }



        // DELETE: api/cart/{userId}
        // Sepeti tamamen temizle
        [HttpDelete("{userId}")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            await _cartService.ClearCartAsync(userId);
            return NoContent();
        }
    }
}
