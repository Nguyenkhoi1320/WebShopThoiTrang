using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models
{
    public class CartItem
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public int quantity { get; set; }
        public Decimal productPrice { get; set; }
        public string anhsp { get; set; }
    }
}
