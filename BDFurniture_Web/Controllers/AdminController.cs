using BDFurniture_Web.Data;
using BDFurniture_Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BDFurniture_Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public AdminController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalProducts = await _db.Products.CountAsync();
            ViewBag.TotalCategories = await _db.Categories.CountAsync();
            ViewBag.TotalMessages = await _db.ContactMessages.Where(m => !m.IsRead).CountAsync();
            ViewBag.TotalInquiries = await _db.ProductInquiries.Where(i => !i.IsResolved).CountAsync();
            ViewBag.TotalSubscribers = await _db.NewsletterSubscribers.CountAsync();
            ViewBag.RecentMessages = await _db.ContactMessages.OrderByDescending(m => m.CreatedAt).Take(5).ToListAsync();
            return View();
        }

        // PRODUCTS
        public async Task<IActionResult> Products(int? categoryId, int page = 1)
        {
            const int pageSize = 12;
            var query = _db.Products
                .Include(p => p.Images)
                .Include(p => p.SubCategory)
                .ThenInclude(s => s.Category)
                .AsQueryable();

            if (categoryId.HasValue)
                query = query.Where(p => p.SubCategory.CategoryId == categoryId.Value);

            var total = await query.CountAsync();
            var products = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.Products = products;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(total / (double)pageSize);
            ViewBag.Categories = await _db.Categories.Where(c => c.IsActive).ToListAsync();
            ViewBag.SelectedCategory = categoryId;
            return View();
        }

        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.Categories = await _db.Categories.Where(c => c.IsActive).Include(c => c.SubCategories).ToListAsync();
            ViewBag.SubCategories = await _db.SubCategories.Where(s => s.IsActive).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(Product model, List<IFormFile> images)
        {
            if (string.IsNullOrWhiteSpace(model.Slug))
            {
                model.Slug = GenerateSlug(model.Name);
            }
            ModelState.Remove("Slug");
            ModelState.Remove("SubCategory");

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _db.Categories.Include(c => c.SubCategories).ToListAsync();
                ViewBag.SubCategories = await _db.SubCategories.ToListAsync();
                return View(model);
            }

            model.CreatedBy = User.Identity?.Name;
            _db.Products.Add(model);
            await _db.SaveChangesAsync();

            // Handle image uploads
            if (images != null && images.Any())
            {
                var uploadPath = Path.Combine(_env.WebRootPath, "uploads", "products");
                Directory.CreateDirectory(uploadPath);
                bool isFirst = true;

                foreach (var file in images)
                {
                    if (file.Length > 0)
                    {
                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                        var filePath = Path.Combine(uploadPath, fileName);
                        using var stream = new FileStream(filePath, FileMode.Create);
                        await file.CopyToAsync(stream);

                        _db.ProductImages.Add(new ProductImage
                        {
                            ProductId = model.Id,
                            ImagePath = $"/uploads/products/{fileName}",
                            IsMain = isFirst,
                            SortOrder = isFirst ? 0 : 1
                        });
                        isFirst = false;
                    }
                }
                await _db.SaveChangesAsync();
            }

            TempData["Success"] = "Product created successfully!";
            return RedirectToAction(nameof(Products));
        }

        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _db.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();
            ViewBag.Categories = await _db.Categories.Include(c => c.SubCategories).ToListAsync();
            ViewBag.SubCategories = await _db.SubCategories.ToListAsync();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(Product model, List<IFormFile> newImages)
        {
            ModelState.Remove("Slug");
            ModelState.Remove("SubCategory");

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _db.Categories.Include(c => c.SubCategories).ToListAsync();
                ViewBag.SubCategories = await _db.SubCategories.ToListAsync();
                return View(model);
            }

            var product = await _db.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == model.Id);
            if (product == null) return NotFound();

            product.Name = model.Name;
            product.ShortDescription = model.ShortDescription;
            product.Description = model.Description;
            product.Price = model.Price;
            product.DiscountPrice = model.DiscountPrice;
            product.SubCategoryId = model.SubCategoryId;
            product.WhatsAppNumber = model.WhatsAppNumber;
            product.IsFeatured = model.IsFeatured;
            product.IsActive = model.IsActive;
            product.UpdatedAt = DateTime.Now;

            if (newImages != null && newImages.Any())
            {
                var uploadPath = Path.Combine(_env.WebRootPath, "uploads", "products");
                Directory.CreateDirectory(uploadPath);
                bool hasMain = product.Images.Any(i => i.IsMain);

                foreach (var file in newImages)
                {
                    if (file.Length > 0)
                    {
                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                        using var stream = new FileStream(Path.Combine(uploadPath, fileName), FileMode.Create);
                        await file.CopyToAsync(stream);
                        _db.ProductImages.Add(new ProductImage
                        {
                            ProductId = product.Id,
                            ImagePath = $"/uploads/products/{fileName}",
                            IsMain = !hasMain
                        });
                        hasMain = true;
                    }
                }
            }

            await _db.SaveChangesAsync();
            TempData["Success"] = "Product updated successfully!";
            return RedirectToAction(nameof(Products));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _db.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == id);
            if (product != null)
            {
                foreach (var img in product.Images)
                {
                    var fullPath = Path.Combine(_env.WebRootPath, img.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(fullPath)) System.IO.File.Delete(fullPath);
                }
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
                TempData["Success"] = "Product deleted.";
            }
            return RedirectToAction(nameof(Products));
        }

        public async Task<IActionResult> DeleteImage(int imageId, int productId)
        {
            var image = await _db.ProductImages.FindAsync(imageId);
            if (image != null)
            {
                var fullPath = Path.Combine(_env.WebRootPath, image.ImagePath.TrimStart('/'));
                if (System.IO.File.Exists(fullPath)) System.IO.File.Delete(fullPath);
                _db.ProductImages.Remove(image);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(EditProduct), new { id = productId });
        }

        // MESSAGES
        public async Task<IActionResult> Messages()
        {
            var messages = await _db.ContactMessages.OrderByDescending(m => m.CreatedAt).ToListAsync();
            return View(messages);
        }

        [HttpPost]
        public async Task<IActionResult> MarkRead(int id)
        {
            var msg = await _db.ContactMessages.FindAsync(id);
            if (msg != null) { msg.IsRead = true; await _db.SaveChangesAsync(); }
            return RedirectToAction(nameof(Messages));
        }

        // INQUIRIES
        public async Task<IActionResult> Inquiries()
        {
            var inquiries = await _db.ProductInquiries.OrderByDescending(i => i.CreatedAt).ToListAsync();
            return View(inquiries);
        }

        // YOUTUBE VIDEOS
        public async Task<IActionResult> Videos()
        {
            var videos = await _db.YouTubeVideos.OrderBy(v => v.SortOrder).ToListAsync();
            return View(videos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVideo(YouTubeVideo model)
        {
            if (ModelState.IsValid)
            {
                _db.YouTubeVideos.Add(model);
                await _db.SaveChangesAsync();
                TempData["Success"] = "Video added.";
            }
            return RedirectToAction(nameof(Videos));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteVideo(int id)
        {
            var video = await _db.YouTubeVideos.FindAsync(id);
            if (video != null) { _db.YouTubeVideos.Remove(video); await _db.SaveChangesAsync(); }
            return RedirectToAction(nameof(Videos));
        }

        // SETTINGS
        public async Task<IActionResult> Settings()
        {
            var settings = await _db.SiteSettings.ToListAsync();
            return View(settings);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSetting(int id, string value)
        {
            var setting = await _db.SiteSettings.FindAsync(id);
            if (setting != null)
            {
                setting.SettingValue = value;
                setting.UpdatedAt = DateTime.Now;
                await _db.SaveChangesAsync();
            }
            return Json(new { success = true });
        }

        // REVIEWS
        public async Task<IActionResult> Reviews()
        {
            var reviews = await _db.ProductReviews
                .Include(r => r.Product)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
            return View(reviews);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveReview(int id)
        {
            var review = await _db.ProductReviews.FindAsync(id);
            if (review != null) { review.IsApproved = true; await _db.SaveChangesAsync(); }
            return RedirectToAction(nameof(Reviews));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _db.ProductReviews.FindAsync(id);
            if (review != null) { _db.ProductReviews.Remove(review); await _db.SaveChangesAsync(); }
            return RedirectToAction(nameof(Reviews));
        }

        private static string GenerateSlug(string name)
        {
            return name.ToLower()
                .Replace(" ", "-")
                .Replace("'", "")
                .Replace(",", "")
                .Replace(".", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("/", "-")
                + "-" + Guid.NewGuid().ToString()[..6];
        }
    }
}
