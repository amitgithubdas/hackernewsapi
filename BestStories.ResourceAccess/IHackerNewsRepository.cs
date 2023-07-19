using BestStories.Entities;

namespace BestStories.ResourceAccess
{
    public interface IHackerNewsRepository
    {
        Task<List<int>> GetBestStoriesAsync();
        Task<Entity?> GetStoryAsync(int id);
    }
}