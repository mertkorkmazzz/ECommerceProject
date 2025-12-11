using AutoMapper;
using ECommerce.Entities.Entities;
using ECommerce.Services.DTOs.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderItem, OrderItemDto>()
         .ForMember(x => x.ProductName, opt => opt.MapFrom(src => src.Product.Name));

            CreateMap<CreateOrderDto, Order>()
                .ForMember(x => x.OrderItems, opt => opt.MapFrom(src => src.Items));

            CreateMap<Order, OrderListDto>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(src => src.User.Name));

            CreateMap<Order, OrderDetailDto>()
                .ForMember(x => x.Items, opt => opt.MapFrom(src => src.OrderItems));

        }
    }
}
