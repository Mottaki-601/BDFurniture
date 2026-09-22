using BDFurniture_Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BDFurniture_Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> _roleManager, UserManager<ApplicationUser> _userManager)
        {
            this._roleManager = _roleManager;
            this._userManager = _userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string userRole)
        {
            string msg = "";
            if (!string.IsNullOrEmpty(userRole)) 
            {
                if (await _roleManager.RoleExistsAsync(userRole))
                {
                    msg = "Role[" + userRole + "] already exist !!!";
                }
                else
                {
                    IdentityRole r = new IdentityRole(userRole);
                    await _roleManager.CreateAsync(r);
                    msg = "Role [" + userRole + "] created successfully !!!!";
                }
            }
            else
            {
                msg = "Please enter a valid role name...!!!";
            }
            ViewBag.msg = msg;
            return View("Index");
        }


        public IActionResult AssignRole()
        {
            ViewBag.users = _userManager.Users;
            ViewBag.roles = _roleManager.Roles;
            ViewBag.msg = TempData["msg"];

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(string userData, string roleData)
        {
            string msg = "";
            if (!string.IsNullOrEmpty(userData) && !string.IsNullOrEmpty(roleData)) 
            {
                ApplicationUser u = await _userManager.FindByEmailAsync(userData);
                if (u != null)
                {
                    if (await _roleManager.RoleExistsAsync(roleData))
                    {
                        await _userManager.AddToRoleAsync(u, roleData);
                        msg = "Role has been assign to user...";
                    }
                    else
                    {
                        msg = "Role does not exist !!!";
                    }
                }
                else
                {
                    msg = "User not found !!!";
                }
            }
            else
            {
                msg = "Please select a valid user and role!!!";
            }
            TempData["msg"] = msg;
            return RedirectToAction("AssignRole");
        }
    }
}
