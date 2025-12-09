using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entities.Entities
{
    public class Payment
    {
        //Ödeme bilgileri

        public int Id { get; set; }


        public int OrderId { get; set; }
        public Order Order { get; set; }


        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public bool IsPaid { get; set; }
    }
}
