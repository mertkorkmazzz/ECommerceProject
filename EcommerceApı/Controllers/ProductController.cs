using ECommerce.Services.Abstract;
using ECommerce.Services.DTOs.ProductDto;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApı.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }


        //IActionResult nedir : controller metotların dönebilceği tüm HTTP cevaplarını temsil eden bir arayüzdür.
        // bu metot, tüm ürünleri asenkron olarak alır ve HTTP 200 OK durum kodu ile birlikte döner.
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products); // Ok : 200 OK HTTP durum kodu ile birlikte verileri döner.ve işlem başarılı olduğunu belirtir.
        }


        // bu metot, belirli bir ürünü kimliğine göre asenkron olarak alır.
        [HttpGet("{ıd:int}")]
        public async Task<IActionResult> GetById(int ıd)
        {
            var product = await _productService.GetByIdAsync(ıd);

            if (product == null)
                return NotFound("ürün bulunamadı");//notfound : 404 HTTP durum kodu ile birlikte hata mesajını döner. ve istenen kaynağın bulunamadığını belirtir.


            return Ok(product);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // BadRequest : 400 HTTP durum kodudur ve yanlış veya eksik istek verilerini belirtir.
            // BadRequest ile notfound arasındaki fark : BadRequest, istemcinin hatalı bir istek gönderdiğini belirtirken, NotFound, istemcinin istediği kaynağın sunucuda bulunamadığını belirtir.

            await _productService.CreateAsync(createDto);
            return Ok("ürün başarılı eklendi");
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductUpdateDto updateDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            await _productService.UpdateAsync(updateDto);
            return Ok("ürün güncellendi");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return Ok("ürün silindi");
        }
    }

}
