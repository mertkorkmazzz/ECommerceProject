using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entities.Entities
{
    public class Shipping
    {
        public int Id { get; set; }


        public int OrderId { get; set; }
        public Order Order { get; set; }


        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public DateTime ShippedDate { get; set; }
        public string Status { get; set; } // Hazırlanıyor, yolda, teslim edildi
    }
}
