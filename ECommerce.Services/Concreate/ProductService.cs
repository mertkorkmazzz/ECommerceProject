using AutoMapper;
using ECommerce.Data.UnitOfWorks;
using ECommerce.Entities.Entities;
using ECommerce.Services.Abstract;
using ECommerce.Services.DTOs.ProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Concreate
{
    public class ProductService : IProductService
    {
        // bağımlılık ları değiştirilmesin ve sadece ctor içinde atanabilsin diye private ve readonly yapıldı
        private readonly IUnitOfWork _unitOf;
        private readonly IMapper _mapper; 

        public ProductService( IUnitOfWork unitOf ,IMapper mapper)
        {
            this._unitOf = unitOf;
            this._mapper = mapper;
        }





        public async Task CreateAsync(ProductCreateDto Dto)
        {
           var product = _mapper.Map<Product>(Dto);

            await _unitOf.Repository<Product>().AddAsync(product);
            await _unitOf.SaveAsync();
        }

        public async Task DeleteAsync(int ıd)
        {
            var product = await _unitOf.Repository<Product>().GetByIdAsync(ıd);

            _unitOf.Repository<Product>().Delete(product);
            await _unitOf.SaveAsync();
        }

        public async Task<List<ProductListDto>> GetAllAsync()
        {
            var product = await _unitOf.Repository<Product>().GetAllAsync();
            return _mapper.Map<List<ProductListDto>>(product);
        }

        public async Task<ProductListDto> GetByIdAsync(int ıd)
        {
            var product = await _unitOf.Repository<Product>().GetByIdAsync(ıd);

            return product == null ? null : _mapper.Map<ProductListDto>(product);
        }

        public async Task UpdateAsync(ProductUpdateDto Dto)
        {
            var product = await _unitOf.Repository<Product>().GetByIdAsync(Dto.Id);

            _mapper.Map(Dto, product);


            _unitOf.Repository<Product>().Update(product);
            await _unitOf.SaveAsync();
        }
    }
}
