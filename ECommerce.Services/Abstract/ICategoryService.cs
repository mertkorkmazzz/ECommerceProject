using ECommerce.Services.DTOs.CategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Abstract
{
    public interface ICategoryService
    {
        Task<List<CategoryListDto>> GetAllAsync();
        Task<CategoryListDto> GetByIdAsync(int id);
        Task CreateAsync(CategoryCreateDto dto);
        Task UpdateAsync(CategoryUpdateDto dto);
        Task DeleteAsync(int id);
      
    }
}
