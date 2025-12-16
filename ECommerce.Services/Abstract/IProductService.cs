using ECommerce.Services.DTOs.ProductDto;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Abstract
{
    public interface IProductService
    {
        Task<List<ProductListDto>> GetAllAsync();
        Task<ProductListDto> GetByIdAsync(int ıd);
        Task CreateAsync(ProductCreateDto Dto);
        Task UpdateAsync(ProductUpdateDto Dto);
        Task DeleteAsync(int ıd);
    }
}
