using Microsoft.AspNetCore.Http;
using PersonalProject.Data;
using PersonalProject.Models.Configuration;
using PersonalProject.Models.DTOs;
using PersonalProject.Models.Extensions;
using PersonalProject.Models.Repositories;
using PersonalProject.Models.ViewModels;
using System.Text;
using System.Text.Json;

namespace PersonalProject.Models
{
    public class Cart
    {
        private const string CartKey = "mycart";
        private const string CountKey = "mycount";

        private ISession session { get; set; }
        private IRequestCookieCollection requestCookies { get; set; }
        private IResponseCookies responseCookies { get; set; }

        private List<CartItem> items { get; set; } = null!;
        private List<CartItemDTO> cookieItems { get; set; } = null!;

        public Cart(HttpContext ctx)
        {
            session = ctx.Session;
            requestCookies = ctx.Request.Cookies;
            responseCookies = ctx.Response.Cookies;
        }
        
        public void Load(ItemRepository<Item> data)
        {
            items = session.GetObject<List<CartItem>>(CartKey) 
                ?? new List<CartItem>();
            cookieItems = requestCookies.GetObject<List<CartItemDTO>>(CartKey) 
                ?? new List<CartItemDTO>();

            if (cookieItems.Count > items.Count)
            {
                items.Clear();

                foreach (CartItemDTO storedItem in cookieItems)
                {
                    var newItem = data.Get(new QueryOptions<Item>
                    {
                        Where = i => i.ItemID == storedItem.ItemID
                    });
                    if (newItem != null)
                    {
                        CartItem item = new()
                        {
                            Item = new ItemDTO(newItem),
                            Quantity = storedItem.Quantity 
                        };
                        items.Add(item);
                    }
                }

            }
        }

        public decimal Subtotal => items.Sum(i => i.Subtotal);
        public int? Count => session.GetInt32(CountKey) ?? requestCookies.GetInt32(CountKey);
        public IEnumerable<CartItem> List => items;

        public CartItem? GetByID(int? id)
        {
            if (items == null || id == null)
            {
                return null;
            }
            else
            {
                return items.FirstOrDefault(ci => ci != null && ci.Item?.ItemID == id);
            }
        }

        public void Add(CartItem item)
        {
            var itemInCart = GetByID(item.Item.ItemID);
            if (itemInCart == null)
            {
                items.Add(item);
            }
            else
            {
                itemInCart.Quantity += 1;
            }
        }

        public void Edit(CartItem item)
        {
            var itemInCart = GetByID(item.Item.ItemID);
            if (itemInCart != null)
            {
                itemInCart.Quantity = item.Quantity;
            }
        }

        public void Remove(CartItem item) => items.Remove(item);

        public void Clear() => items.Clear();

        public void Save()
        {
            if (items.Count == 0)
            {
                session.Remove(CartKey);
                session.Remove(CountKey);
                responseCookies.Delete(CartKey);
                responseCookies.Delete(CountKey);
            }
            else
            {
                session.SetObject<List<CartItem>>(CartKey, items);
                session.SetInt32(CountKey, items.Count);
                responseCookies.SetObject<List<CartItemDTO>>(CartKey, items.ToDTO());
                responseCookies.SetInt32(CountKey, items.Count);
            }
        }
    }
}

