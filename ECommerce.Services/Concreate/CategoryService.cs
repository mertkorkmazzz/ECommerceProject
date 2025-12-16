using AutoMapper;
using ECommerce.Data.UnitOfWorks;
using ECommerce.Entities.Entities;
using ECommerce.Services.Abstract;
using ECommerce.Services.DTOs.CategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Concreate
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOf;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOf , IMapper mapper)
        {
            this._unitOf = unitOf;
            this._mapper = mapper;
        }






        public async Task CreateAsync(CategoryCreateDto dto)
        {
           var Category = _mapper.Map<Category>(dto);

            await _unitOf.Repository<Category>().AddAsync(Category);
            await _unitOf.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _unitOf.Repository<Category>().GetByIdAsync(id);


            _unitOf.Repository<Category>().Delete(product);
            await _unitOf.SaveAsync();
        }

        public async Task<List<CategoryListDto>> GetAllAsync()
        {
            var product = await _unitOf.Repository<Category>().GetAllAsync();

            return _mapper.Map<List<CategoryListDto>>(product);
        }

        public async Task<CategoryListDto> GetByIdAsync(int id)
        {
            var product =  await _unitOf.Repository<Category>().GetByIdAsync(id);

            return product == null ? null :  _mapper.Map<CategoryListDto>(product);
        }

        public async Task UpdateAsync(CategoryUpdateDto dto)
        {
            var product = await _unitOf.Repository<Category>().GetByIdAsync(dto.Id);

            _mapper.Map(dto, product);

            _unitOf.Repository<Category>().Update(product);
            await _unitOf.SaveAsync();
        }
    }
}
