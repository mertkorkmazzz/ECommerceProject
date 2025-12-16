using ECommerce.Services.DTOs.ShippingDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Abstract
{
    // kargo hizmetleri ile ilgili operasyonları tanımlayan arayüz
    public interface IShippingService
    {
        //tüm kargoları listeler
        Task<List<ShippingDetailDto>> GetAllAsync();

        // id'ye göre kargo detayını getirir
        Task<ShippingDetailDto> GetByIdAsync(int id);

        // yeni kargo oluşturur
        Task CreateAsync(ShippingCreateDto dto);

        // mevcut kargo bilgisini günceller : hazırlanıyor, yolda, teslim edildi vb.
        Task UpdateStatusAsync(ShippingUpdateStatusDto dto);

        // id'ye göre kargoyu siler
        Task DeleteAsync(int id);
    }
}
