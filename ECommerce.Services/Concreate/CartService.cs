using AutoMapper;
using ECommerce.Data.UnitOfWorks;
using ECommerce.Entities.Entities;
using ECommerce.Services.Abstract;
using ECommerce.Services.DTOs.CartDto;
using ECommerce.Services.DTOs.CartDtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Concreate
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }





        // kullancı ürün eklemek istediğinde sepet var mı yok mu kontrol et yoksa oluştur
        // varsa aynı üründen var mı kontrol et varsa adet arttır yoksa yeni ürün ekle
        public async Task AddToCartAsync(int userId, AddToCartDto dto)
        {
            // Repositorieleri al
            var cartRepo = _unitOfWork.Repository<Cart>();
            var cartItemRepo = _unitOfWork.Repository<CartItem>();


            var cart = await cartRepo
                .GetQuery()
                .Include(c => c.CartItems) // ınclude ile cartItemları da yükle normalde sadece cart ı getirir
                .FirstOrDefaultAsync(c => c.UserId == userId);// şarta göre getir yani userId ye göre getir

            // Kullanıcının sepeti yoksa oluştur
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId
                };

                await cartRepo.AddAsync(cart);
                await _unitOfWork.SaveAsync();
            }

            // Aynı ürün sepette var mı?
            var cartItem = cart.CartItems
                .FirstOrDefault(ci => ci.ProductId == dto.ProductId);

            if (cartItem != null)
            {
                cartItem.Quantity += dto.Quantity;
                cartItemRepo.Update(cartItem);
            }
            else
            {
                cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity
                };

                await cartItemRepo.AddAsync(cartItem);
            }

            await _unitOfWork.SaveAsync();
        }


        // Tüm sepeti temizle
        // ya sipariş verdik ten sonra ya da kullanıcı sepeti boşaltmak istediğinde temizlenir
        public async Task ClearCartAsync(int userId)
        {
            var cart = await _unitOfWork
              .Repository<Cart>()
              .GetQuery()
              .Include(c => c.CartItems)
              .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                return;

            _unitOfWork.Repository<CartItem>().DeleteRange(cart.CartItems); // delete sadece 1 tane entity silmeye izin verir ama deleranger ile birden fazla entity silebiliriz
            await _unitOfWork.SaveAsync();
        }

        // Kullanıcının sepetini getir
        // yoksa boş sepet döner
        public async Task<CartDto> GetCartAsync(int userId)
        {
            var cart = await _unitOfWork.Repository<Cart>()
              .GetQuery()
              .Include(c => c.CartItems)
                  .ThenInclude(ci => ci.Product) // ThenInclude ne işe yarar : cartItemların içindeki productları da yükler 
              .FirstOrDefaultAsync(c => c.UserId == userId);

            // Kullanıcının sepeti yoksa boş bir sepet döneriz if else lede yapabilirsik ama gereksiz kod olurdu onun yerine kullanıcıaya sadece boş bir sepet döneriz
            if (cart == null)
                return new CartDto 
                {
                    UserId = userId 
                };

            // burda cart entity sini cart dto ya mapliyoruz bu sayede sadece gerekli alanları dönebiliriz
            return _mapper.Map<CartDto>(cart);


        }

        // Sepetten ürün silme
        // kullanıcı sepetten ürün silmek istediğinde çağrılır
        public async Task RemoveFromCartAsync(int userId, int productId)
        {
            var cart = await _unitOfWork
               .Repository<Cart>()
               .GetQuery()
               .Include(c => c.CartItems)
               .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                return;

            var cartItem = cart.CartItems
                .FirstOrDefault(ci => ci.ProductId == productId);

            if (cartItem == null)
                return;

            _unitOfWork.Repository<CartItem>().Delete(cartItem);
            await _unitOfWork.SaveAsync();
        }
    }
}
