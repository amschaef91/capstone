namespace PersonalProject.Models.ViewModels
{
    public class OrderViewModel
    {
        public Order Order { get; set; } = new Order();
        public Customer Customer { get; set; } = new Customer();
        public IEnumerable<CartItem> List { get; set; } = new List<CartItem>();
        public decimal Subtotal { get; set; }
    }
}
