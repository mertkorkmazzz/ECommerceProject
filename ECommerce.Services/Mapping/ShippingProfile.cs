using AutoMapper;
using ECommerce.Entities.Entities;
using ECommerce.Services.DTOs.ShippingDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Mapping
{
    public class ShippingProfile : Profile
    {
        public ShippingProfile()
        {
            CreateMap<ShippingCreateDto, Shipping>();

            CreateMap<Shipping, ShippingDetailDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<ShippingUpdateStatusDto, Shipping>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.ShippingId));
        }
    }
}
