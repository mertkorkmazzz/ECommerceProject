using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entities.Entities
{
    public class Order
    {
        // Kullanıcın verdiği siprişleri temsil eder 
        public int Id { get; set; }


        // siparişi veren kullanıcı
        public int UserId { get; set; }
        public User User { get; set; }



        public DateTime OrderDate { get; set; }// sipariş tarihi
        public decimal TotalPrice { get; set; }//Toplam Fiyat


        public ICollection<OrderItem> OrderItems { get; set; } //Sipariş detayları 
    }
}
