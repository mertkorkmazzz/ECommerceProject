using ECommerce.Services.Abstract;
using ECommerce.Services.DTOs.UserDto;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace EcommerceApı.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {


        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }





        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var users = await _userService.GetByIdAsync(id);

            if (users == null)
                return NotFound();


            return Ok(users);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateDto createDto)
        {
           if(!ModelState.IsValid)
                return BadRequest(ModelState);

           await _userService.CreateAsync(createDto);
            return StatusCode(StatusCodes.Status201Created);
            // statuscode : asp.net core un hazırladığı durum kodları(status code)
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _userService.UpdateAync(dto);
            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
