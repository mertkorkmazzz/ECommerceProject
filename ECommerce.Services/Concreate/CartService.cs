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






        public async Task AddToCartAsync(int userId, AddToCartDto dto)
        {
            var cartRepo = _unitOfWork.Repository<Cart>();
            var cartItemRepo = _unitOfWork.Repository<CartItem>();

            var cart = await cartRepo
                .GetQuery()
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

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

        public async Task ClearCartAsync(int userId)
        {
            var cart = await _unitOfWork
              .Repository<Cart>()
              .GetQuery()
              .Include(c => c.CartItems)
              .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                return;

            _unitOfWork.Repository<CartItem>().DeleteRange(cart.CartItems); // delete de hata verdi 2 tane olduğu için deleteRange yaptım(araştır)
            await _unitOfWork.SaveAsync();
        }

        public async Task<CartDto> GetCartAsync(int userId)
        {
            var cart = await _unitOfWork.Repository<Cart>()
              .GetQuery()
              .Include(c => c.CartItems)
                  .ThenInclude(ci => ci.Product)
              .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                return new CartDto { UserId = userId };

            return _mapper.Map<CartDto>(cart);


        }

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
