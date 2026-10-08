namespace BDFurniture_Web.Services
{
    public class YouTubeShortDto
    {
        public string VideoId { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? ThumbnailUrl { get; set; }

        public DateTime PublishedAt { get; set; }

        public string VideoUrl =>
            $"https://www.youtube.com/shorts/{VideoId}";
    }

    public class YouTubeShortsPagedResult
    {
        public List<YouTubeShortDto> Shorts { get; set; } = new();

        public string? NextPageToken { get; set; }

        public int TotalResults { get; set; }
    }
}