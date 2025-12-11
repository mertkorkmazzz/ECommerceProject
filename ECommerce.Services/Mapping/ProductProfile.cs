using AutoMapper;
using ECommerce.Entities.Entities;
using ECommerce.Services.DTOs.ProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            
            CreateMap<Product, ProductListDto>()
          .ForMember(x => x.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Product, ProductDetailDto>()
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Product, ProductUpdateDto>();
        }
    }
}
