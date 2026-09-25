using BDFurniture_Web.Data;
using BDFurniture_Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BDFurniture_Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        // GET: /collection/{slug} - Category page
        [Route("collection/{slug}")]
        public async Task<IActionResult> Category(string slug)
        {
            var category = await _db.Categories
                .Include(c => c.SubCategories)
                .FirstOrDefaultAsync(c => c.Slug == slug && c.IsActive);

            if (category == null) return NotFound();

            var products = await _db.Products
                .Include(p => p.Images)
                .Include(p => p.SubCategory)
                .Where(p => p.SubCategory.CategoryId == category.Id && p.IsActive)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            ViewBag.Category = category;
            ViewBag.Products = products;
            ViewBag.AllCategories = await _db.Categories.Where(c => c.IsActive).OrderBy(c => c.SortOrder).ToListAsync();
            return View();
        }

        // GET: /collection/{categorySlug}/{subSlug} - SubCategory page
        [Route("collection/{categorySlug}/{subSlug}")]
        public async Task<IActionResult> SubCategory(string categorySlug, string subSlug)
        {
            var category = await _db.Categories
                .FirstOrDefaultAsync(c => c.Slug == categorySlug && c.IsActive);
            if (category == null) return NotFound();

            var subCategory = await _db.SubCategories
                .FirstOrDefaultAsync(s => s.Slug == subSlug && s.CategoryId == category.Id && s.IsActive);
            if (subCategory == null) return NotFound();

            var products = await _db.Products
                .Include(p => p.Images)
                .Where(p => p.SubCategoryId == subCategory.Id && p.IsActive)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            ViewBag.Category = category;
            ViewBag.SubCategory = subCategory;
            ViewBag.Products = products;
            return View();
        }

        // GET: /product/{slug}
        [Route("product/{slug}")]
        public async Task<IActionResult> Detail(string slug)
        {
            var product = await _db.Products
                .Include(p => p.Images)
                .Include(p => p.SubCategory)
                .ThenInclude(s => s.Category)
                .Include(p => p.Reviews.Where(r => r.IsApproved))
                .Include(p => p.Ratings)
                .FirstOrDefaultAsync(p => p.Slug == slug && p.IsActive);

            if (product == null) return NotFound();

            // Increment view count
            product.ViewCount++;
            await _db.SaveChangesAsync();

            // Related products
            ViewBag.RelatedProducts = await _db.Products
                .Include(p => p.Images)
                .Where(p => p.SubCategoryId == product.SubCategoryId && p.Id != product.Id && p.IsActive)
                .Take(4)
                .ToListAsync();

            // Current user's rating
            if (User.Identity?.IsAuthenticated == true)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    ViewBag.UserRating = await _db.ProductRatings
                        .FirstOrDefaultAsync(r => r.ProductId == product.Id && r.UserId == user.Id);
                }
            }

            return View(product);
        }

        // POST: Rate product
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rate(int productId, int rating)
        {
            if (rating < 1 || rating > 5)
                return Json(new { success = false, message = "Invalid rating." });

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Json(new { success = false, message = "Please login to rate." });

            var product = await _db.Products
                .Include(p => p.Ratings)
                .FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null) return Json(new { success = false, message = "Product not found." });

            var existingRating = product.Ratings.FirstOrDefault(r => r.UserId == user.Id);

            if (existingRating != null)
            {
                // Rating rule: if 5+ ratings exist, can only increase
                if (product.TotalRatings >= 5 && rating < existingRating.Rating)
                    return Json(new { success = false, message = "You cannot decrease the rating after 5 reviews have been submitted." });

                existingRating.Rating = rating;
                existingRating.UpdatedAt = DateTime.Now;
            }
            else
            {
                _db.ProductRatings.Add(new ProductRating { ProductId = productId, UserId = user.Id, Rating = rating });
            }

            await _db.SaveChangesAsync();

            // Recalculate average
            var allRatings = await _db.ProductRatings.Where(r => r.ProductId == productId).ToListAsync();
            product.AverageRating = (decimal)allRatings.Average(r => r.Rating);
            product.TotalRatings = allRatings.Count;
            await _db.SaveChangesAsync();

            return Json(new { success = true, average = product.AverageRating, total = product.TotalRatings });
        }

        // POST: Submit inquiry
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendInquiry(ProductInquiry model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Please fill all required fields." });

            var product = await _db.Products.FindAsync(model.ProductId);
            if (product != null) model.ProductName = product.Name;

            _db.ProductInquiries.Add(model);
            await _db.SaveChangesAsync();
            return Json(new { success = true });
        }
    }
}
