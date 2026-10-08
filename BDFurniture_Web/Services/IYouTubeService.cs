namespace BDFurniture_Web.Services
{
    public interface IYouTubeService
    {
        Task<List<YouTubeShortDto>> GetLatestShortsAsync(
            string channelUrl,
            int count = 12);

        Task<YouTubeShortsPagedResult> GetPagedShortsAsync(
            string channelUrl,
            string? pageToken = null,
            int count = 12);
    }
}

