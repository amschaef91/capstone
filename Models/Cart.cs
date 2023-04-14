using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProject.Models
{
    public class Cart
    {
        private List<Item> products;

        public Cart()
        {
            products = new List<Item>();
        }
        
        public void AddItem(Item product)
        {
            products.Add(product);
        }

        public void RemoveItem(Item product)
        {
            products.Remove(product);
        }
        public List<Item> GetProducts()
        {
            return products;
        }

        public decimal GetSubTotal()
        {
            decimal subtotal = 0;
            foreach(var product in products)
            {
                subtotal += product.Price;
            }
            return subtotal;
        }
    }
}
