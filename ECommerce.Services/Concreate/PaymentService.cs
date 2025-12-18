using AutoMapper;
using ECommerce.Data.UnitOfWorks;
using ECommerce.Entities.Entities;
using ECommerce.Services.Abstract;
using ECommerce.Services.DTOs.PaymentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Concreate
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }


        // ödeme oluşturma
        public async Task<PaymentResultDto> CreatePaymentAsync(PaymentCreateDto dto)
        {
            var payment = _mapper.Map<Payment>(dto);
            payment.PaymentDate = DateTime.Now;
            payment.IsPaid = true; 


            await _unitOfWork.Repository<Payment>().AddAsync(payment);
            await _unitOfWork.SaveAsync();


            return new PaymentResultDto
            {
                PaymentId = payment.Id,
                IsPaid = payment.IsPaid,
                PaymentDate = payment.PaymentDate
            };
        }


        // ödeme durumu sorgulama
        public async Task<PaymentResultDto> GetPaymentStatusAsync(int paymentId)
        {
            var payment = await _unitOfWork.Repository<Payment>().GetByIdAsync(paymentId);

            if (payment == null)
                throw new Exception("ödeme bulunamadı");


            return new PaymentResultDto
            {
                PaymentId = payment.Id,
                IsPaid = payment.IsPaid,
                PaymentDate = payment.PaymentDate
            };
        }
    }
}
