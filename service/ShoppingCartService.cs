using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service
{
    public class ShoppingCartService
    {
        private List<CartItem> items;

        public ShoppingCartService()
        {
            items = new List<CartItem>();
        }

        public void AddItem(int productId, string productName, decimal price, int quantity)
        {
            var existingItem = items.FirstOrDefault(item => item.productId == productId);

            if (existingItem != null)
            {
                existingItem.quantity += quantity;
            }
            else
            {
                items.Add(new CartItem
                {
                    productId = productId,
                    productName = productName,
                    productPrice = price,
                    quantity = quantity
                });
            }
        }
    }
}
