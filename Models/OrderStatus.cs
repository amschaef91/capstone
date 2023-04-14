using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProject.Models
{
    public enum Status
    {
        PENDING,
        CONFIRMED,
        SHIPPED,
        DELIVERED,
        CANCELLED
    }
    public class OrderStatus
    {
        public int OrderID { get; set; }
        public Status Status { get; set; }
        public Order Order { get; set; }

        public OrderStatus() { }
        public OrderStatus(int orderID, Status status, Order order)
        {
            OrderID = orderID;
            Status = status;
            Order = order;
        }
    }
}
