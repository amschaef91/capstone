using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProject.Models
{
    public class Order
    {

        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Order() { }

        public Order(int orderID, int customerID, decimal total, Customer customer, DateTime orderDate, DateTime? shipDate)
        {
            OrderID = orderID;
            CustomerID = customerID;
            Total = total;
            Customer = customer;
            OrderDate = orderDate;
            ShipDate = shipDate;
        }
    }
}
