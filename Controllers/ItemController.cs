using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PersonalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalProject.Data;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using PersonalProject.Areas.Admin.Models;
using PersonalProject.Models.ViewModels;

namespace PersonalProject.Controllers
{
    public class ItemController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ItemController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult List()
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

    }
}