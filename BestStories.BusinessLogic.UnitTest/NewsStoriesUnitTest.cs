using BestStories.BusinessLogic.Implementation;
using BestStories.Entities;
using BestStories.ResourceAccess;
using FluentAssertions;
using Moq;
using System.Collections.Generic;

namespace BestStories.BusinessLogic.UnitTest
{
    public class NewsStoriesUnitTest
    {
        

        [Fact]
        public async Task ShouldReturnIsValidResponseFromGetNewsStroriesAsync()
        {
            var repoMock = new Mock<IHackerNewsRepository>();
            List<int> ids = new List<int> { 1, 2, 3 };
            List<Entity> list = new List<Entity> { new Entity { Id= 36757542, Title = "Llama 2", Url = "https://ai.meta.com/llama/", By = "friggeri", Score = 2189 } };
            repoMock.Setup(p => p.GetBestStoriesAsync()).ReturnsAsync(ids);
            repoMock.Setup(c => c.GetStoryAsync(1)).ReturnsAsync(new Entity { Id = 36757542, Title = "Llama 2", Url = "https://ai.meta.com/llama/", By = "friggeri", Score = 2189 });
            repoMock.Setup(c => c.GetStoryAsync(2)).ReturnsAsync(new Entity { Id = 36774627, Title = "Llama 3", Url = "https://ai.meta.com/llama/", By = "friggeri", Score = 2190 });
            repoMock.Setup(c => c.GetStoryAsync(3)).ReturnsAsync(new Entity { Id = 36753225, Title = "Llama 4", Url = "https://ai.meta.com/llama/", By = "friggeri", Score = 2178 });
            var sut = new NewsStories(repoMock.Object);
            var result = await sut.GetNewsStroriesAsync(1);
            result.Should().BeEquivalentTo(list);
        }
    }

}