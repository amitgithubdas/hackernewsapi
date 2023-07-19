using BestStories.BusinessLogic;
using BestStories.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace HackerNewsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BestStoriesController : ControllerBase
    {
        private readonly INewsStories newsStories;
        //private readonly IMemoryCache memoryCache;

        //public BestStoriesController(INewsStories newsStories, IMemoryCache memoryCache)
        //{
        //    this.newsStories = newsStories;
        //    this.memoryCache = memoryCache;
        //}
        public BestStoriesController(INewsStories newsStories)
        {
            this.newsStories = newsStories;
        }

        [HttpGet]
        public async Task<IEnumerable<Story>> Get(int numberOfBestStories)
        {
            List<Story> stories = await GetBestStories(numberOfBestStories);
            return stories.ToArray();
        }

        private async Task<List<Story>> GetBestStories(int numberOfBestStories)
        {
            List<Story> stories = new List<Story>();
            var result = await newsStories.GetNewsStroriesAsync(numberOfBestStories);
            stories = (from data in result
                       select new Story
                       {
                           title = data.Title,
                           uri = data.Url,
                           Time = DateTime.UnixEpoch.AddSeconds(data.Time),
                           commentcount = data.Descendants,
                           postedby = data.By,
                           score = data.Score
                       }).ToList<Story>();
            return stories;
        }
    }
}