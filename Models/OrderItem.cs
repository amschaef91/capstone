using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProject.Models
{
    public class OrderItem
    {
        public int OrderID { get; set; }
        public Order? Order { get; set; }
        public int ItemID { get; set; }
        public string Name { get; set; }
        public Item? Item { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }


    }
}
