using Microsoft.AspNetCore.Mvc;
using PersonalProject.Data;
using PersonalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalProject.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace PersonalProject.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]
    public class InventoryController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public InventoryController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Inventory()
        {
            var items = _context.Items.ToList();
            return View(items);
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
                _context.Items.Add(item);
                await _context.SaveChangesAsync();

                int itemID = item.ItemID;
                if (model.Images != null && model.Images.Count > 5)
                {
                    ModelState.AddModelError(string.Empty, "You can only upload up to 5 images.");
                    return View(model);
                }

                if (model.Images != null && model.Images.Count > 0)
                {
                    var imagePath = "/images/" + itemID + "/";
                    var directoryPath = _environment.WebRootPath + imagePath;

                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    var images = new List<ItemImages>();
                    foreach (var image in model.Images)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                        var savePath = Path.Combine(directoryPath, fileName);
                        using (var fileStream = new FileStream(savePath, FileMode.Create))
                        {
                            image.CopyTo(fileStream);
                        }

                        images.Add(new ItemImages
                        {
                            ItemID = itemID,
                            ImgPath = imagePath + fileName
                        }
                        );
                    }
                    _context.ItemImages.AddRange(images);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
