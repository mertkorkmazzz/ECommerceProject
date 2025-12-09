using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entities.Entities
{
    public class Cart
    {

        // kullanıcı sipariş vermeden önceki sepet bilgilerini tutar
        public int Id { get; set; }


        public int UserId { get; set; }
        public User User { get; set; }


        public ICollection<CartItem> CartItems { get; set; }
    }
}
