namespace PersonalProject.Models.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<CartItem> List { get; set; } = new List<CartItem>();
        public decimal Subtotal { get; set; }
    }
}
