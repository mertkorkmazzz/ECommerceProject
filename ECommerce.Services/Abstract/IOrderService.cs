using ECommerce.Services.DTOs.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Abstract
{
    public interface IOrderService
    {
        // Kullanıcının sepetindeki ürünleri veya belirli bir siparişi veritabanına kaydetmek için kullanılır.
        Task CreateOrderAsync(CreateOrderDto createOrderDto);

        // kullacının siparişlerini getirir
        Task<List<OrderListDto>> GetOrdersByUserIdAsync(int userId);

        // siparişin detaylarını getirir
        Task<OrderDetailDto> GetOrderDetailAsync(int orderId);

        // Sistem üzerindeki tüm siparişleri getirir.
        Task<List<OrderListDto>> GetAllOrdersAsync();
    }
}
