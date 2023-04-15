using PersonalProject.Models.DTOs;
using System.Text.Json.Serialization;

namespace PersonalProject.Models.ViewModels
{
    public class CartItem
    {
        public ItemDTO Item { get; set; } = new ItemDTO();
        public int Quantity { get; set; }

        [JsonIgnore]
        public decimal Subtotal => Item.Price * Quantity;
    }
}
