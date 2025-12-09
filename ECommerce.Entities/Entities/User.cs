using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entities.Entities
{
    public class User
    {
        //Müşteri
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PssswordHash { get; set; }


        // her kullanıcının birden fazla siparişi olabilir
        public ICollection<Order> Orders { get; set; }
    }
}
