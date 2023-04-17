using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalProject.Data;
using PersonalProject.Models;
using PersonalProject.Models.Repositories;
using PersonalProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProject.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private ItemRepository<Item> data { get; set; }


        public OrderController(ApplicationDbContext context, UserManager<User> usrMgr)
        {
            _userManager = usrMgr;
            _context = context;
            data = new ItemRepository<Item>(context);
        }

        private Cart GetCart()
        {
            var cart = new Cart(HttpContext);
            cart.Load(data);
            return cart;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Sales,Shipping")]
        public IActionResult ViewAll()
        {
            var items = _context.Orders.Include(i => i.OrderID);
            return View(items);
        }
        public async Task<IActionResult> Checkout()
        {
            Cart cart = GetCart();
            var user = await _userManager.GetUserAsync(User);
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerID == user.CustomerID);
            var model = new OrderViewModel
            {
                Customer = customer,
                List = cart.List,
                Subtotal = cart.Subtotal
            };
            return View(model);
        } 
    }
}
