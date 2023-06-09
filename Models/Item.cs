﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public List<ItemImages> ItemImages { get; set; } = new List<ItemImages>();

        public Item(){}

        public Item(string name, string description, decimal price, int stock)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
        }
    }
}
