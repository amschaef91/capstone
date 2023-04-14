using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PersonalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalProject.Data;
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

        public IActionResult Index()
        {
            return View();
        }



        public async Task<IActionResult> AddItem(ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                Item item = new()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Stock = model.Stock
                };

                if (model.Images != null && model.Images.Count > 5)
                {
                    ModelState.AddModelError(string.Empty, "You can only upload up to 5 images.");
                    return View(model);
                }

                if (model.Images != null && model.Images.Count > 0)
                {
                    item.ItemImages = new List<ItemImages>();
                    foreach (var image in model.Images)
                    {
                        var imagePath = "/images/" + model.Name + "/" + image.FileName;
                        var savePath = _environment.WebRootPath + imagePath;
                        Directory.CreateDirectory(savePath);

                        using (var fileStream = new FileStream(savePath, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }

                        item.ItemImages.Add(new ItemImages
                        {
                            ImgPath = imagePath
                        });
                    }
                }

                _context.Items.Add(item);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}