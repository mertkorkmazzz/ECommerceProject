using AutoMapper;
using ECommerce.Data.UnitOfWorks;
using ECommerce.Entities.Entities;
using ECommerce.Services.Abstract;
using ECommerce.Services.DTOs.ShippingDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Services.Concreate
{
    public class ShippingService : IShippingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShippingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateAsync(ShippingCreateDto dto)
        {
            var shipping = _mapper.Map<Shipping>(dto);

            // Başlangıç durumu ve tarih ataması
            shipping.Status = dto.Status ?? "Hazırlanıyor";
            shipping.ShippedDate = DateTime.UtcNow;
            shipping.CreatedDate = DateTime.UtcNow;

            await _unitOfWork.Repository<Shipping>().AddAsync(shipping);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var shipping = await _unitOfWork.Repository<Shipping>().GetByIdAsync(id);
            if (shipping == null)
                throw new Exception("Böyle bir kargo bulunamadı");

            _unitOfWork.Repository<Shipping>().Delete(shipping);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<ShippingDetailDto>> GetAllAsync()
        {
            var shippings = await _unitOfWork.Repository<Shipping>().GetAllAsync();
            return _mapper.Map<List<ShippingDetailDto>>(shippings);
        }

        public async Task<ShippingDetailDto> GetByIdAsync(int id)
        {
            var shipping = await _unitOfWork.Repository<Shipping>().GetByIdAsync(id);
            if (shipping == null)
                throw new Exception("Böyle bir kargo bulunamadı");

            return _mapper.Map<ShippingDetailDto>(shipping);
        }

        public async Task UpdateStatusAsync(ShippingUpdateStatusDto dto)
        {
            var shipping = await _unitOfWork.Repository<Shipping>().GetByIdAsync(dto.ShippingId);
            if (shipping == null)
                throw new Exception("Güncellenecek kargo bulunamadı");

            shipping.Status = dto.Status;
            _unitOfWork.Repository<Shipping>().Update(shipping);
            await _unitOfWork.SaveAsync();
        }
    }
}
