using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace BDFurniture_Web.Services
{
    public class YouTubeService : IYouTubeService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<YouTubeService> _logger;

        public YouTubeService(
            IConfiguration configuration,
            ILogger<YouTubeService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<List<YouTubeShortDto>> GetLatestShortsAsync(
            string channelUrl,
            int count = 10)
        {
            var result = new List<YouTubeShortDto>();

            try
            {
                // 1. API Key
                var apiKey = _configuration["YouTube:ApiKey"];

                if (string.IsNullOrWhiteSpace(apiKey))
                {
                    _logger.LogError(
                        "YouTube API key is not configured.");

                    return result;
                }

                // 2. Channel URL check
                if (string.IsNullOrWhiteSpace(channelUrl))
                {
                    _logger.LogWarning(
                        "YouTube channel URL is empty.");

                    return result;
                }

                // 3. Create YouTube API client
                var youtube =
                    new Google.Apis.YouTube.v3.YouTubeService(
                        new BaseClientService.Initializer
                        {
                            ApiKey = apiKey,
                            ApplicationName = "BD Furniture"
                        });

                // 4. Extract @handle
                var handle = ExtractChannelHandle(channelUrl);

                if (string.IsNullOrWhiteSpace(handle))
                {
                    _logger.LogWarning(
                        "Invalid YouTube channel URL: {Url}",
                        channelUrl);

                    return result;
                }

                // =========================================
                // 5. CHANNEL LIST
                // =========================================

                var channelRequest =
                    youtube.Channels.List(
                        "id,contentDetails");

                channelRequest.ForHandle = handle;

                var channelResponse =
                    await channelRequest.ExecuteAsync();

                var channel =
                    channelResponse.Items.FirstOrDefault();

                if (channel == null)
                {
                    _logger.LogWarning(
                        "YouTube channel not found: {Handle}",
                        handle);

                    return result;
                }

                // =========================================
                // 6. GET UPLOADS PLAYLIST ID
                // =========================================

                var uploadsPlaylistId =
                    channel.ContentDetails
                           .RelatedPlaylists
                           .Uploads;

                if (string.IsNullOrWhiteSpace(
                    uploadsPlaylistId))
                {
                    return result;
                }

                // =========================================
                // 7. PLAYLIST ITEMS LIST
                // =========================================

                var playlistRequest =
                    youtube.PlaylistItems.List(
                        "snippet,contentDetails");

                playlistRequest.PlaylistId =
                    uploadsPlaylistId;

                playlistRequest.MaxResults =
                    Math.Min(count * 2, 50);

                var playlistResponse =
                    await playlistRequest.ExecuteAsync();

                var videoIds =
                    playlistResponse.Items
                        .Where(x =>
                            x.ContentDetails != null &&
                            !string.IsNullOrWhiteSpace(
                                x.ContentDetails.VideoId))
                        .Select(x =>
                            x.ContentDetails.VideoId)
                        .Distinct()
                        .ToList();

                if (!videoIds.Any())
                    return result;

                // =========================================
                // 8. VIDEO LIST
                // =========================================

                var videoRequest =
                    youtube.Videos.List(
                        "snippet,contentDetails");

                videoRequest.Id =
                    string.Join(",", videoIds);

                var videoResponse =
                    await videoRequest.ExecuteAsync();

                // =========================================
                // 9. FILTER SHORTS
                // =========================================

                foreach (var video in videoResponse.Items)
                {
                    if (video.Snippet == null ||
                        video.ContentDetails == null)
                    {
                        continue;
                    }

                    var duration =
                        ParseYouTubeDuration(
                            video.ContentDetails.Duration);

                    // 1 minute or less
                    if (duration > TimeSpan.FromMinutes(1))
                        continue;

                    var thumbnail =
                        video.Snippet.Thumbnails?.High?.Url
                        ?? video.Snippet.Thumbnails?.Medium?.Url
                        ?? video.Snippet.Thumbnails?.Default__?.Url;

                    result.Add(new YouTubeShortDto
                    {
                        VideoId = video.Id,

                        Title = video.Snippet.Title,

                        Description =
                            video.Snippet.Description,

                        ThumbnailUrl = thumbnail,

                        PublishedAt =
                            video.Snippet
                                .PublishedAtDateTimeOffset?
                                .DateTime
                            ?? DateTime.MinValue
                    });

                    if (result.Count >= count)
                        break;
                }

                // =========================================
                // 10. NEWEST FIRST
                // =========================================

                return result
                    .OrderByDescending(x => x.PublishedAt)
                    .Take(count)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error while loading YouTube Shorts.");

                return result;
            }
        }

        // =============================================
        // Extract @handle from YouTube URL
        // =============================================

        private static string? ExtractChannelHandle(
            string channelUrl)
        {
            if (string.IsNullOrWhiteSpace(channelUrl))
                return null;

            var atIndex =
                channelUrl.IndexOf('@');

            if (atIndex < 0)
                return null;

            var handle =
                channelUrl[(atIndex + 1)..]
                    .Split('/', '?', '#')
                    .FirstOrDefault();

            return string.IsNullOrWhiteSpace(handle)
                ? null
                : handle;
        }

        // =============================================
        // Convert YouTube ISO duration
        // =============================================

        private static TimeSpan ParseYouTubeDuration(
            string? duration)
        {
            if (string.IsNullOrWhiteSpace(duration))
                return TimeSpan.Zero;

            try
            {
                return System.Xml.XmlConvert
                    .ToTimeSpan(duration);
            }
            catch
            {
                return TimeSpan.Zero;
            }
        }
    }
}