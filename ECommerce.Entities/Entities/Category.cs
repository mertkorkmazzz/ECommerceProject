using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entities.Entities
{
    public class Category
    {
        //KATEGORİLER
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        // bir kategorinin birden fazla ürünü olabilir
        public ICollection<Product> Products { get; set; }
    }
}
