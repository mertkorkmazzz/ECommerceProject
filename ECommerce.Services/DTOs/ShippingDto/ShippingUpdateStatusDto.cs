using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.DTOs.ShippingDto
{
    public class ShippingUpdateStatusDto
    {
        public int ShippingId { get; set; }
        public string Status { get; set; }
    }
}
