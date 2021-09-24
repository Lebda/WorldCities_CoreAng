using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WorldCities.Models.RequestFeatures;

namespace WorldCities.Implementations.RequestFeatures
{
    public static class IQueryableExtensions
    {
        public static async Task<PagedList<T>> ToPagedListAsync<T>(
            this IQueryable<T> source,
            QueryMetaData metaData)
            where T : class
        {
            var count = await source.CountAsync();
            var items = await source
                .Skip((metaData.PageIndex - (metaData.IsZeroBase ? 0 : 1)) * metaData.PageSize)
                .Take(metaData.PageSize).ToListAsync();
            return new PagedList<T>(items, count, metaData);
        }
    }
}
