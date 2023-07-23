using BestStories.Entities;
using Microsoft.Extensions.Caching.Memory;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace BestStories.ResourceAccess.Implementation
{
    public class HackerNewsRepository : IHackerNewsRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache memoryCache;
        public HackerNewsRepository(HttpClient client, IMemoryCache memoryCache)
        {
            this._httpClient = client;
            this.memoryCache = memoryCache;
        }
        public async Task<List<int>> GetBestStoriesAsync()
        {
            using (var response = await _httpClient.GetAsync("beststories.json"))
            {
                response.EnsureSuccessStatusCode();
                var res = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<int>>(res) ?? new List<int>();
            }
            
        }
        /// <summary>
        /// GetStoryAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Entity?> GetStoryAsync(int id)
        {
            return this.memoryCache.GetOrCreateAsync(id, async e =>
            {
                using (var response = await _httpClient.GetAsync($"item/{id}.json"))
                {
                    response.EnsureSuccessStatusCode();
                    var res = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    e.SlidingExpiration = new TimeSpan(0,1,0);
                    return JsonSerializer.Deserialize<Entity>(res, options);
                }
            });
        }
    }
}