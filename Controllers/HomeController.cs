using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalProject.Data;
using PersonalProject.Models;
using PersonalProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var items = from i in _context.Items
                        join ii in _context.ItemImages on i.ItemID equals ii.ItemID into itemImages
                        select new ItemImageViewModel
                        {
                            Item = new Item()
                            {
                                ItemID = i.ItemID,
                                Name = i.Name,
                                Description = i.Description,
                                Price = i.Price,
                            },
                            ItemImages = i.ItemImages.ToList()

                        };
            return View(items);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
