using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProject.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
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
        public IActionResult Checkout()
        {
            return View();
        } 
    }
}
