using AutoMapper;
using ECommerce.Entities.Entities;
using ECommerce.Services.DTOs.PaymentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Mapping
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<PaymentCreateDto, Payment>();

            CreateMap<Payment, PaymentResultDto>()
                .ForMember(x => x.PaymentId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
