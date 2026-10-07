namespace BDFurniture_Web.Services
{
    public interface IYouTubeService
    {
        Task<List<YouTubeShortDto>> GetLatestShortsAsync(
        string channelUrl,
        int count = 10);
    }
}
