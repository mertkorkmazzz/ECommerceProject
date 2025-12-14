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
//AUTOMAPPER nedir
// AutoMapper, .NET uygulamalarında nesneler arasında veri dönüşümünü kolaylaştıran popüler bir kütüphanedir.
// Özellikle veri transfer nesneleri (DTO'lar) ile varlık modelleri (Entity'ler) arasında veri kopyalamak için kullanılır.
// AutoMapper, kaynak nesnenin özelliklerini hedef nesnenin karşılık gelen özelliklerine otomatik olarak eşler ve böylece manuel olarak veri kopyalama ihtiyacını ortadan kaldırır.



//Profile nedir
// Profile, AutoMapper kütüphanesinde kullanılan bir sınıftır ve nesneler arasındaki eşleme (mapping) yapılandırmalarını tanımlamak için kullanılır.
// yani mapping kurrallarını tutan bir yerdir
//mapping kurllarını saklamak ve auto mapper a bildirmek için kullanılır


//createMap nedir
//Hangi nesnenin hangi nesneye nasıl gideceğini tanımlayan fonksiyon.


//forMember nedir
// Belirli bir üyenin (property) nasıl eşleneceğini yapılandırmak için kullanılır.
// Örneğin, bir kaynaktaki "FirstName" özelliğini hedefteki "Name" özelliğine eşlemek için kullanılabilir.
// .ForMember(x => x.PropertyName, opt => opt.MapFrom(src => src.OtherPropertyName))


//mapFrom nedir
// Kaynak nesneden hedef nesneye veri kopyalama işlemini tanımlamak için kullanılır.
// Örneğin, bir kaynaktaki "FirstName" özelliğini hedefteki "Name" özelliğine eşlemek için kullanılabilir.
// .ForMember(x => x.PropertyName, opt => opt.MapFrom(src => src.OtherPropertyName))


//forMember ile mapFrom arasındaki fark nedir
// forMember, hedef nesnedeki belirli bir üyenin (property) nasıl eşleneceğini yapılandırmak için kullanılır.
// mapFrom ise, forMember içinde kullanılarak, kaynak nesneden hedef nesneye veri kopyalama işlemini tanımlamak için kullanılır.
// Yani, forMember hedefteki üyeyi belirtirken, mapFrom kaynaktaki verinin nereden geleceğini belirtir.