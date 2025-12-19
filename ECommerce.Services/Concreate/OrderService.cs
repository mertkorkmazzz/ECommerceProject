using AutoMapper;
using ECommerce.Data.UnitOfWorks;
using ECommerce.Entities.Entities;
using ECommerce.Services.Abstract;
using ECommerce.Services.DTOs.OrderDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Concreate
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }



        public async Task<Order> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            var order = _mapper.Map<Order>(createOrderDto);
            order.OrderDate = DateTime.Now;

            foreach (var item in order.OrderItems)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.ProductId);
                if (product == null)
                    throw new Exception($"Product ID {item.ProductId} bulunamadı");

                item.UnitPrice = (int)product.Price;
            }

            await _unitOfWork.Repository<Order>().AddAsync(order);
            await _unitOfWork.SaveAsync();

            return order; // Order nesnesini döndür
        }

        // Sistem üzerindeki tüm siparişleri getirir.
        public async Task<List<OrderListDto>> GetAllOrdersAsync()
        {
            var orders = await _unitOfWork.Repository<Order>()
                .GetQuery()
                .Include(o => o.User)
                .ToListAsync();

            return _mapper.Map<List<OrderListDto>>(orders);
        }

        // siparişin detaylarını getirir
        public async Task<OrderDetailDto> GetOrderDetailAsync(int orderId)
        {
            var order = await _unitOfWork.Repository<Order>()
              .GetQuery()
              .Include(o => o.OrderItems)
                  .ThenInclude(oi => oi.Product)
              .Include(o => o.User)
              .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                throw new Exception("Sipariş bulunamadı.");

            return _mapper.Map<OrderDetailDto>(order);

        }


        // kullacının siparişlerini getirir
        public async Task<List<OrderListDto>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _unitOfWork.Repository<Order>().GetAllAsync();

            var userorders = orders
                .Where(o => o.UserId == userId)
                .ToList();


            return _mapper.Map<List<OrderListDto>>(userorders);
        }

        
    }
}
