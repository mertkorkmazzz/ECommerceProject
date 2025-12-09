using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entities.Entities
{
    public class Product
    {
        //ÜRÜNLER
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }



        // her ürün bir kategoriye aittir
        public int CategoryId { get; set; }
        public Category Category { get; set; }



        // bir ürünün birden fazla sipariş kalemi olabilir
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
