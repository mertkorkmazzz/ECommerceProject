using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entities.Entities
{
    public class OrderItem
    {
        // siparişin bir öğesini detayını temsil eder
        public int Id { get; set; }


        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }



        public int Quantity { get; set; }//ürün adedi
        public int UnitPrice { get; set; }//fiyatlar güncellenebildiği için ürünün o anki fiyatını temsil eder
    }
}
