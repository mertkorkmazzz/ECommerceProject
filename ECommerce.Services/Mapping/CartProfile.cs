using AutoMapper;
using ECommerce.Entities.Entities;
using ECommerce.Services.DTOs.CartDto;
using ECommerce.Services.DTOs.CartDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Mapping
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CartItem, CartItemDto>()
                  .ForMember(x => x.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                  .ForMember(x => x.Price, opt => opt.MapFrom(src => src.Product.Price));

            CreateMap<Cart, CartDto>()
                .ForMember(x => x.Items, opt => opt.MapFrom(src => src.CartItems))
                .ForMember(x => x.TotalPrice, opt => opt.MapFrom(src =>
                    src.CartItems.Sum(i => i.Quantity * i.Product.Price)
                ));

            CreateMap<AddToCartDto, CartItem>();
        }
    }
}
