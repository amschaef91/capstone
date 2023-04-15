using PersonalProject.Models.DTOs;
using PersonalProject.Models.ViewModels;

namespace PersonalProject.Models.Extensions
{
    public static class CartItemExtensions
    {
        public static List<CartItemDTO> ToDTO(this List<CartItem> list) =>
            list.Select(ci => new CartItemDTO
            {
                ItemID = ci.Item.ItemID,
                Quantity = ci.Quantity
            }).ToList();
    }
}
