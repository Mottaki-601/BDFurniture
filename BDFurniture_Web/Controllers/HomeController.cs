using BDFurniture_Web.Data;
using BDFurniture_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BDFurniture_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext db, ILogger<HomeController> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Categories = await _db.Categories
                .Where(c => c.IsActive)
                .OrderBy(c => c.SortOrder)
                .ToListAsync();

            ViewBag.FeaturedProducts = await _db.Products
                .Include(p => p.Images)
                .Include(p => p.SubCategory)
                .ThenInclude(s => s.Category)
                .Where(p => p.IsActive && p.IsFeatured)
                .OrderByDescending(p => p.CreatedAt)
                .Take(8)
                .ToListAsync();

            ViewBag.YouTubeVideos = await _db.YouTubeVideos
                .Where(v => v.IsActive)
                .OrderBy(v => v.SortOrder)
                .ToListAsync();

            ViewBag.Reviews = await _db.ProductReviews
                .Where(r => r.IsApproved)
                .OrderByDescending(r => r.CreatedAt)
                .Take(6)
                .ToListAsync();

            ViewBag.Settings = await _db.SiteSettings.ToListAsync();

            return View();
        }

        public IActionResult Career()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendContact(ContactMessage model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Please fill all required fields." });

            _db.ContactMessages.Add(model);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Message sent successfully!" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(string email, string? name)
        {
            if (string.IsNullOrEmpty(email))
                return Json(new { success = false, message = "Please enter a valid email." });

            var existing = await _db.NewsletterSubscribers
                .FirstOrDefaultAsync(s => s.Email == email);

            if (existing != null)
                return Json(new { success = false, message = "You are already subscribed!" });

            _db.NewsletterSubscribers.Add(new NewsletterSubscriber { Email = email, Name = name });
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Subscribed successfully! Thank you." });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
