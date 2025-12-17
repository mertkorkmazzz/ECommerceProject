using ECommerce.Services.DTOs.CartDto;
using ECommerce.Services.DTOs.CartDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Abstract
{
    public interface ICartService
    {
        // Belirtilen kullanıcıya ait mevcut sepeti getirir.
        Task<CartDto> GetCartAsync(int userId);

        //Kullanıcının sepetine ürün ekler.
        Task AddToCartAsync(int userId, AddToCartDto dto);

        //Kullanıcının sepetinden belirli bir ürünü tamamen kaldırır
        Task RemoveFromCartAsync(int userId, int productId);

        //Kullanıcının sepetindeki tüm ürünleri temizler.
        Task ClearCartAsync(int userId);
    }
}
