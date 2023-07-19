using BestStories.Entities;
using BestStories.ResourceAccess;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestStories.BusinessLogic.Implementation
{
    public class NewsStories : INewsStories
    {
        private readonly IHackerNewsRepository repository;

        public NewsStories(IHackerNewsRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<List<Entity>> GetNewsStroriesAsync(int noOfNewsStories)
        {
            var storyIds = await repository.GetBestStoriesAsync();
            var entitiesRecords = await Task.WhenAll(storyIds.Take(noOfNewsStories).Select(stId => repository.GetStoryAsync(stId)));
            return entitiesRecords?.Where(p => p is not null).Cast<Entity>().ToList()?? new List<Entity>();
        }
    }
}
