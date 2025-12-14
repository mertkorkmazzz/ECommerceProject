using ECommerce.Services.DTOs.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Abstract
{
    public interface IUserService
    {
        Task<List<UserListDto>> GetAllAsync();
        Task<UserListDto> GetByIdAsync(int id);
        Task CreateAsync(UserCreateDto dto);
        Task UpdateAync(UserUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
