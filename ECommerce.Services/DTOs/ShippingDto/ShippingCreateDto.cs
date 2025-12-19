using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.DTOs.ShippingDto
{
    public class ShippingCreateDto
    {
        public int OrderId { get; set; }
        public string City { get; set; }
        public string Address { get; set; }       // Siparişin teslim adresi
        public string ShippingType { get; set; }  // Örn: Standard, Express
        public string Status { get; set; }        // Örn: Hazırlanıyor, Kargoya Verildi
        public DateTime EstimatedDelivery { get; set; }
        public string PostalCode { get; set; }
    }
}
