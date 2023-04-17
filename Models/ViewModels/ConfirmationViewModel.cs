namespace PersonalProject.Models.ViewModels
{
    public class ConfirmationViewModel
    {
        public Customer Customer { get; set; }
        public User User { get; set; }
        public Order Order { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
