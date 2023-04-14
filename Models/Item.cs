using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProject.Models
{
    public class Item
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public List<ItemImages> ItemImages { get; set; }

        public Item(){}

        public Item(int itemID, string name, string description, decimal price, int stock)
        {
            ItemID = itemID;
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
        }
    }
}
