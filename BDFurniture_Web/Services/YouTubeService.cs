using System.Collections.Concurrent;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace BDFurniture_Web.Services
{
    public class YouTubeService : IYouTubeService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<YouTubeService> _logger;
        private static readonly ConcurrentDictionary<string, string> _uploadsPlaylistCache = new();

        public YouTubeService(
            IConfiguration configuration,
            ILogger<YouTubeService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<List<YouTubeShortDto>> GetLatestShortsAsync(
            string channelUrl,
            int count = 12)
        {
            var paged = await GetPagedShortsAsync(channelUrl, null, count);
            return paged.Shorts;
        }

        public async Task<YouTubeShortsPagedResult> GetPagedShortsAsync(
            string channelUrl,
            string? pageToken = null,
            int count = 12)
        {
            var pagedResult = new YouTubeShortsPagedResult();

            try
            {
                // 1. Check API Key
                var apiKey = _configuration["YouTube:ApiKey"];
                if (string.IsNullOrWhiteSpace(apiKey))
                {
                    _logger.LogWarning("YouTube API key is not configured.");
                    return pagedResult;
                }

                // 2. Check Channel URL
                if (string.IsNullOrWhiteSpace(channelUrl))
                {
                    _logger.LogWarning("YouTube channel URL is empty.");
                    return pagedResult;
                }

                // 3. Create YouTube API client
                var youtube = new Google.Apis.YouTube.v3.YouTubeService(
                    new BaseClientService.Initializer
                    {
                        ApiKey = apiKey,
                        ApplicationName = "BD Furniture"
                    });

                // 4. Resolve Uploads Playlist ID (with caching)
                var uploadsPlaylistId = await GetUploadsPlaylistIdAsync(youtube, channelUrl);
                if (string.IsNullOrWhiteSpace(uploadsPlaylistId))
                {
                    _logger.LogWarning("Could not resolve uploads playlist for channel URL: {Url}", channelUrl);
                    return pagedResult;
                }

                // 5. Fetch Playlist Items with Pagination
                var playlistRequest = youtube.PlaylistItems.List("snippet,contentDetails");
                playlistRequest.PlaylistId = uploadsPlaylistId;
                playlistRequest.MaxResults = Math.Clamp(count * 2, 10, 50);
                if (!string.IsNullOrWhiteSpace(pageToken))
                {
                    playlistRequest.PageToken = pageToken;
                }

                var playlistResponse = await playlistRequest.ExecuteAsync();
                pagedResult.NextPageToken = playlistResponse.NextPageToken;
                pagedResult.TotalResults = playlistResponse.PageInfo?.TotalResults ?? 0;

                var videoIds = playlistResponse.Items?
                    .Where(x => x.ContentDetails != null && !string.IsNullOrWhiteSpace(x.ContentDetails.VideoId))
                    .Select(x => x.ContentDetails.VideoId)
                    .Distinct()
                    .ToList() ?? new List<string>();

                if (!videoIds.Any())
                {
                    return pagedResult;
                }

                // 6. Fetch Video Details (durations and thumbnails)
                var videoRequest = youtube.Videos.List("snippet,contentDetails");
                videoRequest.Id = string.Join(",", videoIds);

                var videoResponse = await videoRequest.ExecuteAsync();
                if (videoResponse.Items == null || !videoResponse.Items.Any())
                {
                    return pagedResult;
                }

                // 7. Parse & Filter for Shorts (up to 3 minutes / 180s)
                var shortsList = new List<YouTubeShortDto>();
                var fallbackList = new List<YouTubeShortDto>();

                foreach (var video in videoResponse.Items)
                {
                    if (video.Snippet == null || video.ContentDetails == null)
                        continue;

                    var duration = ParseYouTubeDuration(video.ContentDetails.Duration);

                    var thumbnail = video.Snippet.Thumbnails?.Maxres?.Url
                        ?? video.Snippet.Thumbnails?.High?.Url
                        ?? video.Snippet.Thumbnails?.Medium?.Url
                        ?? video.Snippet.Thumbnails?.Default__?.Url
                        ?? $"https://img.youtube.com/vi/{video.Id}/hqdefault.jpg";

                    var dto = new YouTubeShortDto
                    {
                        VideoId = video.Id,
                        Title = video.Snippet.Title ?? "BD Furniture Short",
                        Description = video.Snippet.Description,
                        ThumbnailUrl = thumbnail,
                        PublishedAt = video.Snippet.PublishedAtDateTimeOffset?.DateTime ?? DateTime.MinValue
                    };

                    fallbackList.Add(dto);

                    // Shorts format: <= 180 seconds (3 mins)
                    if (duration <= TimeSpan.FromSeconds(180))
                    {
                        shortsList.Add(dto);
                    }
                }

                var finalVideos = shortsList.Any() ? shortsList : fallbackList;

                // Ensure newest uploads appear first
                pagedResult.Shorts = finalVideos
                    .OrderByDescending(x => x.PublishedAt)
                    .Take(count)
                    .ToList();

                return pagedResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while loading YouTube Shorts (pageToken: {PageToken})", pageToken);
                return pagedResult;
            }
        }

        private async Task<string?> GetUploadsPlaylistIdAsync(Google.Apis.YouTube.v3.YouTubeService youtube, string channelUrl)
        {
            if (_uploadsPlaylistCache.TryGetValue(channelUrl, out var cachedId))
            {
                return cachedId;
            }

            var channelRequest = youtube.Channels.List("id,contentDetails");

            if (channelUrl.Contains("/channel/"))
            {
                var id = channelUrl.Substring(channelUrl.IndexOf("/channel/") + 9).Split('/', '?', '#')[0];
                channelRequest.Id = id;
            }
            else
            {
                var handle = ExtractChannelHandle(channelUrl);
                if (string.IsNullOrWhiteSpace(handle))
                {
                    handle = channelUrl.Split('/').LastOrDefault(s => !string.IsNullOrWhiteSpace(s))?.Split('?', '#')[0];
                }

                if (string.IsNullOrWhiteSpace(handle))
                {
                    return null;
                }

                channelRequest.ForHandle = handle.StartsWith("@") ? handle : "@" + handle;
            }

            var channelResponse = await channelRequest.ExecuteAsync();
            var channel = channelResponse.Items?.FirstOrDefault();

            if (channel == null && !string.IsNullOrEmpty(channelRequest.ForHandle))
            {
                var rawHandle = channelRequest.ForHandle.TrimStart('@');
                var retryRequest = youtube.Channels.List("id,contentDetails");
                retryRequest.ForHandle = rawHandle;
                var retryResponse = await retryRequest.ExecuteAsync();
                channel = retryResponse.Items?.FirstOrDefault();
            }

            var uploadsPlaylistId = channel?.ContentDetails?.RelatedPlaylists?.Uploads;
            if (!string.IsNullOrWhiteSpace(uploadsPlaylistId))
            {
                _uploadsPlaylistCache[channelUrl] = uploadsPlaylistId;
            }

            return uploadsPlaylistId;
        }

        private static string? ExtractChannelHandle(string channelUrl)
        {
            if (string.IsNullOrWhiteSpace(channelUrl))
                return null;

            var atIndex = channelUrl.IndexOf('@');
            if (atIndex < 0)
                return null;

            var handle = channelUrl[(atIndex + 1)..]
                .Split('/', '?', '#')
                .FirstOrDefault();

            return string.IsNullOrWhiteSpace(handle)
                ? null
                : "@" + handle.Trim();
        }

        private static TimeSpan ParseYouTubeDuration(string? duration)
        {
            if (string.IsNullOrWhiteSpace(duration))
                return TimeSpan.Zero;

            try
            {
                return System.Xml.XmlConvert.ToTimeSpan(duration);
            }
            catch
            {
                return TimeSpan.Zero;
            }
        }
    }
}

