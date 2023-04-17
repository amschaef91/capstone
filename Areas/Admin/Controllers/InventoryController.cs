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
using PersonalProject.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

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
            var items = _context.Items.Include(i => i.ItemImages).Select(i => new ItemImageViewModel
            {
                Item = i,
                ItemImages = i.ItemImages.ToList()
            });
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

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var model = new ItemImageViewModel
            {
                Item = _context.Items.FirstOrDefault(i => i.ItemID == id),
                ItemImages = _context.ItemImages.Where(img => img.ItemID == id).ToList()
            };
            if (model == null) {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Item item)
        {
            if (id != item.ItemID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var images = _context.ItemImages.Where(img => img.ItemID == id).ToList();
                    int mainImageId = int.Parse(Request.Form["MainImage"]);
                    foreach (var img in images)
                    {
                        if (img.ID.Equals(mainImageId))
                        {
                            img.IsMain = true;
                        }
                        else
                        {
                            img.IsMain = false;
                        }

                        _context.Update(img);
                    }
                    await _context.SaveChangesAsync();
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!(_context.Items.Any(i => i.ItemID == id)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Inventory));
            }
            return View(item);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
     
            var model = new ItemImageViewModel
            {
                Item = _context.Items.FirstOrDefault(i => i.ItemID == id),
                ItemImages = _context.ItemImages.Where(img => img.ItemID == id).ToList()
            };

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id, Item item)
        {
            if (id != item.ItemID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Remove(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                   if( !_context.Items.Any(i =>  !i.ItemID.Equals(id))) { return NotFound(); }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Inventory));
            }
            return View(item);
        }
    }
}
