using AutoMapper;
using ECommerce.Data.UnitOfWorks;
using ECommerce.Entities.Entities;
using ECommerce.Services.Abstract;
using ECommerce.Services.DTOs.ShippingDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Concreate
{
    public class ShippingService : IShippingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShippingService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }






        
        public async Task CreateAsync(ShippingCreateDto dto)
        {
            var shipping = _mapper.Map<Shipping>(dto);

            // başlangıç durumu atandı
            shipping.Status = "hazırlanıyor";
            shipping.ShippedDate = DateTime.UtcNow;

            await _unitOfWork.Repository<Shipping>().AddAsync(shipping);
            await _unitOfWork.SaveAsync();
        }
        
        public async Task DeleteAsync(int id)
        {
            var shipping =  await _unitOfWork.Repository<Shipping>().GetByIdAsync(id);

            if (shipping == null)
                throw new Exception("böyle bir kargo bulunamadı");


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

            if(shipping == null)
                throw new Exception("böyle bir kargo bulunamadı");

            return _mapper.Map<ShippingDetailDto>(shipping);
        }

        public async Task UpdateStatusAsync(ShippingUpdateStatusDto dto)
        {
            var shipping = await _unitOfWork.Repository<Shipping>().GetByIdAsync(dto.ShippingId);

            if (shipping == null)
            {
                throw new Exception("güncellenecek kargo bulunamadı");
            }


            shipping.Status = dto.Status;
            _unitOfWork.Repository<Shipping>().Update(shipping);
            await _unitOfWork.SaveAsync();
        }
    }
}
