using BDFurniture_Web.Data;
using BDFurniture_Web.Models;
using BDFurniture_Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BDFurniture_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<HomeController> _logger;
        private readonly IYouTubeService _youTubeService;
        private readonly IMemoryCache _memoryCache;

        public HomeController(ApplicationDbContext db, ILogger<HomeController> logger, IYouTubeService youTubeService, IMemoryCache memoryCache)
        {
            _db = db;
            _logger = logger;
            _youTubeService = youTubeService;
            _memoryCache = memoryCache;
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

            // YouTube Channel URL
            var ytChannelUrl = await _db.SiteSettings
                .Where(x => x.SettingKey == "YouTubeChannelUrl")
                .Select(x => x.SettingValue)
                .FirstOrDefaultAsync() ?? "https://youtube.com/@bdfurniture";

            // Cache First Page of YouTube Shorts for 30 minutes for blazing fast speed
            var cacheKey = $"YT_ShortsPage_{ytChannelUrl}_first";
            if (!_memoryCache.TryGetValue(cacheKey, out YouTubeShortsPagedResult? pagedShorts) || pagedShorts == null || !pagedShorts.Shorts.Any())
            {
                pagedShorts = await _youTubeService.GetPagedShortsAsync(ytChannelUrl, null, 12);
                if (pagedShorts != null && pagedShorts.Shorts.Any())
                {
                    _memoryCache.Set(cacheKey, pagedShorts, TimeSpan.FromMinutes(30));
                }
            }

            var shorts = pagedShorts?.Shorts ?? new List<YouTubeShortDto>();
            var nextPageToken = pagedShorts?.NextPageToken;

            // Fallback to database YouTubeVideos if API returned empty
            if (!shorts.Any())
            {
                var dbVideos = await _db.YouTubeVideos
                    .Where(v => v.IsActive)
                    .OrderBy(v => v.SortOrder)
                    .ToListAsync();

                shorts = dbVideos.Select(v => new YouTubeShortDto
                {
                    VideoId = v.YouTubeId,
                    Title = v.Title,
                    Description = v.Description,
                    ThumbnailUrl = string.IsNullOrEmpty(v.ThumbnailUrl)
                        ? $"https://img.youtube.com/vi/{v.YouTubeId}/hqdefault.jpg"
                        : v.ThumbnailUrl,
                    PublishedAt = v.CreatedAt
                }).ToList();
            }

            // Ensure newly uploaded videos are first
            shorts = shorts.OrderByDescending(s => s.PublishedAt).ToList();

            ViewBag.YouTubeShorts = shorts;
            ViewBag.NextPageToken = nextPageToken;
            ViewBag.YouTubeChannelUrl = ytChannelUrl;

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

        [HttpGet]
        public async Task<IActionResult> GetMoreShorts(string? pageToken = null)
        {
            try
            {
                var ytChannelUrl = await _db.SiteSettings
                    .Where(x => x.SettingKey == "YouTubeChannelUrl")
                    .Select(x => x.SettingValue)
                    .FirstOrDefaultAsync() ?? "https://youtube.com/@bdfurniture";

                var cacheKey = $"YT_ShortsPage_{ytChannelUrl}_{pageToken ?? "first"}";
                if (!_memoryCache.TryGetValue(cacheKey, out YouTubeShortsPagedResult? pagedShorts) || pagedShorts == null)
                {
                    pagedShorts = await _youTubeService.GetPagedShortsAsync(ytChannelUrl, pageToken, 12);
                    if (pagedShorts != null && pagedShorts.Shorts.Any())
                    {
                        _memoryCache.Set(cacheKey, pagedShorts, TimeSpan.FromMinutes(30));
                    }
                }

                if (pagedShorts == null || !pagedShorts.Shorts.Any())
                {
                    return Json(new { success = true, shorts = Array.Empty<object>(), nextPageToken = (string?)null });
                }

                var items = pagedShorts.Shorts.Select(s => new
                {
                    videoId = s.VideoId,
                    title = s.Title,
                    thumbnailUrl = s.ThumbnailUrl,
                    publishedAt = s.PublishedAt.ToString("dd MMM yyyy"),
                    videoUrl = s.VideoUrl
                });

                return Json(new
                {
                    success = true,
                    shorts = items,
                    nextPageToken = pagedShorts.NextPageToken
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching more YouTube shorts with pageToken: {PageToken}", pageToken);
                return Json(new { success = false, message = "Could not load more shorts." });
            }
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

