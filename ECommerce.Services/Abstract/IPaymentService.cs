using ECommerce.Services.DTOs.PaymentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Abstract
{
    public interface IPaymentService
    {

        Task<PaymentResultDto> CreatePaymentAsync(PaymentCreateDto dto);
        Task<PaymentResultDto> GetPaymentStatusAsync(int paymentId);
    }
}
