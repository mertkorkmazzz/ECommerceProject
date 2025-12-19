using ECommerce.Services.Abstract;
using ECommerce.Services.DTOs.CategoryDto;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApı.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {


        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

     

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
                return NotFound("Kategori bulunamadı");

            return Ok(category);
        }

     
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _categoryService.CreateAsync(createDto);
            return StatusCode(StatusCodes.Status201Created);
        }

      
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CategoryUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _categoryService.UpdateAsync(updateDto);
            return NoContent(); // 204
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return NoContent(); // 204
        }
    }
}
