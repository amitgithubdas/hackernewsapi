using BestStories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestStories.BusinessLogic
{
    public interface INewsStories
    {
        Task<List<Entity>> GetNewsStroriesAsync(int noOfNewsStories);
    }
}
