using Microsoft.AspNetCore.Mvc;
using PersonalProject.Models.Repositories;
using PersonalProject.Models;
using PersonalProject.Data;
using PersonalProject.Models.ViewModels;
using PersonalProject.Models.Configuration;
using PersonalProject.Models.DTOs;

namespace PersonalProject.Controllers
{
    public class CartController : Controller
    {
        private ItemRepository<Item> data { get; set; }

        public CartController(ApplicationDbContext context) => data = new ItemRepository<Item>(context);

        private Cart GetCart()
        {
            var cart = new Cart(HttpContext);
            cart.Load(data);
            return cart;
        }

        public IActionResult Index()
        {
            Cart cart = GetCart();

            var model = new CartViewModel
            {
                List = cart.List,
                Subtotal = cart.Subtotal
            };

            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Add(int id)
        {
            var item = data.Get(new QueryOptions<Item>
            {
                Where = i => i.ItemID == id
            });

            if (item == null)
            {
                TempData["message"] = "Unable to add item to cart";
            }
            else
            {
                CartItem cItem = new()
                {
                    Item = new ItemDTO(item),
                    Quantity = 1
                };

                Cart cart = GetCart();
                cart.Add(cItem);
                cart.Save();

                TempData["message"] = $"{item.Name} added to cart";
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public RedirectToActionResult Remove(int id)
        {
            Cart cart = GetCart();
            CartItem? item = cart.GetByID(id);
            if(item != null) {
            cart.Remove(item);
                cart.Save();
                TempData["message"] = $"{item.Item.Name} removed from cart";
            }
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public RedirectToActionResult Clear()
        {
            Cart cart = GetCart();
            cart.Clear();
            cart.Save();

            TempData["message"] = "Cart cleared.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public RedirectToActionResult Edit(CartItem cartItem)
        {
            Cart cart = GetCart();
            cart.Edit(cartItem);
            cart.Save();

            TempData["message"] = $"{cartItem.Item.Name} updated";
            return RedirectToAction("Index");
        }

        public ViewResult Checkout() => View();
    }

}

