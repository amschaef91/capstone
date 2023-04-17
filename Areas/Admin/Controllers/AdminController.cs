using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalProject.Areas.Admin.Models;
using PersonalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public AdminController(UserManager<User> userMngr, RoleManager<Role> roleMngr)
        {
            userManager = userMngr;
            roleManager = roleMngr;
        }

        public async Task<IActionResult> Index()
        {
            List<User> users = new List<User>();
            foreach (User user in userManager.Users)
            {
                user.RoleNames = await userManager.GetRolesAsync(user);
                users.Add(user);
            }

            // Sort the list by checking if the user's name is "Admin"
            users = users.OrderBy(u => u.UserName != "admin").ToList();

            UserViewModel model = new UserViewModel
            {
                Users = users,
                Roles = roleManager.Roles
            };

            return View(model);
        }

        public async Task<IActionResult> AddToRole(UserViewModel model, string id)
        {
            Role role = await roleManager.FindByNameAsync(model.RoleName);
            User user = await userManager.FindByIdAsync(id);

            if (role == null || user == null)
            {
                TempData["ErrorMessage"] = "Role or user not found.";
                return RedirectToAction("Index");
            }

            await userManager.AddToRoleAsync(user, role.Name);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveRole(UserViewModel model, string id)
        {
            Role role = await roleManager.FindByNameAsync(model.RoleName);
            User user = await userManager.FindByIdAsync(id);

            if (role == null || user == null)
            {
                TempData["ErrorMessage"] = "Role or user not found.";
                return RedirectToAction("Index");
            }

            await userManager.RemoveFromRoleAsync(user, role.Name);
            return RedirectToAction("Index");
        }

    }
}
