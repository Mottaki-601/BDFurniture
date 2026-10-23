using BDFurniture_Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BDFurniture_Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        private static bool IsSystemRole(string? roleName)
        {
            return string.Equals(roleName, "Admin",
                       StringComparison.OrdinalIgnoreCase)
                || string.Equals(roleName, "Customer",
                       StringComparison.OrdinalIgnoreCase);
        }

        // GET: /Role
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles
                .OrderBy(r => r.Name)
                .ToListAsync();

            var roleUserCounts = new Dictionary<string, int>();

            foreach (var role in roles)
            {
                var users = await _userManager.GetUsersInRoleAsync(
                    role.Name!);

                roleUserCounts[role.Id] = users.Count;
            }

            ViewBag.roles = roles;
            ViewBag.roleUserCounts = roleUserCounts;
            ViewBag.msg = TempData["msg"];
            ViewBag.error = TempData["error"];

            return View();
        }

        // POST: /Role/CreateRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(string userRole)
        {
            userRole = (userRole ?? "").Trim();

            if (string.IsNullOrWhiteSpace(userRole)
                || userRole.Length < 2
                || userRole.Length > 50)
            {
                TempData["error"] =
                    "Role name must be between 2 and 50 characters.";

                return RedirectToAction(nameof(Index));
            }

            if (await _roleManager.RoleExistsAsync(userRole))
            {
                TempData["error"] =
                    $"Role '{userRole}' already exists.";

                return RedirectToAction(nameof(Index));
            }

            var role = new IdentityRole(userRole);
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                TempData["msg"] =
                    $"Role '{userRole}' created successfully.";
            }
            else
            {
                TempData["error"] = string.Join(
                    " ", result.Errors.Select(e => e.Description));
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Role/EditRole/id
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                TempData["error"] = "Role not found.";
                return RedirectToAction(nameof(Index));
            }

            if (IsSystemRole(role.Name))
            {
                TempData["error"] =
                    "Admin and Customer are protected system roles.";

                return RedirectToAction(nameof(Index));
            }

            ViewBag.error = TempData["error"];
            return View(role);
        }

        // POST: /Role/EditRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(
            string id, string roleName)
        {
            roleName = (roleName ?? "").Trim();

            if (string.IsNullOrWhiteSpace(roleName)
                || roleName.Length < 2
                || roleName.Length > 50)
            {
                TempData["error"] =
                    "Role name must be between 2 and 50 characters.";

                return RedirectToAction(nameof(EditRole), new { id });
            }

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                TempData["error"] = "Role not found.";
                return RedirectToAction(nameof(Index));
            }

            if (IsSystemRole(role.Name))
            {
                TempData["error"] =
                    "Admin and Customer cannot be renamed.";

                return RedirectToAction(nameof(Index));
            }

            if (IsSystemRole(roleName))
            {
                TempData["error"] =
                    "This name is reserved for a system role.";

                return RedirectToAction(nameof(EditRole), new { id });
            }

            var existingRole =
                await _roleManager.FindByNameAsync(roleName);

            if (existingRole != null && existingRole.Id != role.Id)
            {
                TempData["error"] =
                    $"Role '{roleName}' already exists.";

                return RedirectToAction(nameof(EditRole), new { id });
            }

            role.Name = roleName;

            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                TempData["msg"] =
                    $"Role successfully renamed to '{roleName}'.";
            }
            else
            {
                TempData["error"] = string.Join(
                    " ", result.Errors.Select(e => e.Description));
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: /Role/DeleteRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                TempData["error"] = "Role not found.";
                return RedirectToAction(nameof(Index));
            }

            if (IsSystemRole(role.Name))
            {
                TempData["error"] =
                    "Admin and Customer roles cannot be deleted.";

                return RedirectToAction(nameof(Index));
            }

            var users = await _userManager.GetUsersInRoleAsync(
                role.Name!);

            if (users.Count > 0)
            {
                TempData["error"] =
                    $"Cannot delete '{role.Name}'. " +
                    "Remove its user assignments first.";

                return RedirectToAction(nameof(Index));
            }

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                TempData["msg"] =
                    $"Role '{role.Name}' deleted successfully.";
            }
            else
            {
                TempData["error"] = string.Join(
                    " ", result.Errors.Select(e => e.Description));
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Role/AssignRole
        [HttpGet]
        public async Task<IActionResult> AssignRole()
        {
            var users = await _userManager.Users
                .OrderBy(u => u.Email)
                .ToListAsync();

            var roles = await _roleManager.Roles
                .OrderBy(r => r.Name)
                .ToListAsync();

            var userRoleMap = new Dictionary<string, IList<string>>();

            foreach (var user in users)
            {
                userRoleMap[user.Id] =
                    await _userManager.GetRolesAsync(user);
            }

            ViewBag.users = users;
            ViewBag.roles = roles;
            ViewBag.userRoleMap = userRoleMap;
            ViewBag.msg = TempData["msg"];
            ViewBag.error = TempData["error"];

            return View();
        }

        // POST: /Role/AssignRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(
            string userData, string roleData)
        {
            if (string.IsNullOrWhiteSpace(userData)
                || string.IsNullOrWhiteSpace(roleData))
            {
                TempData["error"] =
                    "Please select both a user and a role.";

                return RedirectToAction(nameof(AssignRole));
            }

            var user = await _userManager.FindByEmailAsync(userData);
            var role = await _roleManager.FindByNameAsync(roleData);

            if (user == null)
            {
                TempData["error"] = "User not found.";
                return RedirectToAction(nameof(AssignRole));
            }

            if (role == null)
            {
                TempData["error"] = "Role not found.";
                return RedirectToAction(nameof(AssignRole));
            }

            if (await _userManager.IsInRoleAsync(user, role.Name!))
            {
                TempData["error"] =
                    $"User already has the '{role.Name}' role.";

                return RedirectToAction(nameof(AssignRole));
            }

            var result = await _userManager.AddToRoleAsync(
                user, role.Name!);

            if (result.Succeeded)
            {
                TempData["msg"] =
                    $"Role '{role.Name}' assigned to '{user.Email}'.";
            }
            else
            {
                TempData["error"] = string.Join(
                    " ", result.Errors.Select(e => e.Description));
            }

            return RedirectToAction(nameof(AssignRole));
        }

        // POST: /Role/RemoveRoleFromUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveRoleFromUser(
            string userId, string roleName)
        {
            if (string.IsNullOrWhiteSpace(userId)
                || string.IsNullOrWhiteSpace(roleName))
            {
                TempData["error"] = "Invalid user or role.";
                return RedirectToAction(nameof(AssignRole));
            }

            if (IsSystemRole(roleName))
            {
                TempData["error"] =
                    "Admin and Customer assignments are protected.";

                return RedirectToAction(nameof(AssignRole));
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                TempData["error"] = "User not found.";
                return RedirectToAction(nameof(AssignRole));
            }

            var role = await _roleManager.FindByNameAsync(roleName);

            if (role == null)
            {
                TempData["error"] = "Role not found.";
                return RedirectToAction(nameof(AssignRole));
            }

            var result = await _userManager.RemoveFromRoleAsync(
                user, role.Name!);

            if (result.Succeeded)
            {
                TempData["msg"] =
                    $"Role '{role.Name}' removed from '{user.Email}'.";
            }
            else
            {
                TempData["error"] = string.Join(
                    " ", result.Errors.Select(e => e.Description));
            }

            return RedirectToAction(nameof(AssignRole));
        }
    }
}