using System.Linq;
using WorldCities.Models.Models;
using WorldCities.Models.RequestFeatures;
using System.Linq.Dynamic.Core;
using WorldCities.Implementations.RequestFeatures;

namespace WorldCities.Implementations.Repository
{
    public static class CityRepositoryExtensions
    {
        public static IQueryable<City> Sort(this IQueryable<City> source, CityRequestParameters input)
        {
            string sortColumn = SortColumn(input.QueryMetaData.SortColumn);
            string sortOrder = LinqDynamicExtensions.SortOrder(input.QueryMetaData.SortOrder);
            return source.OrderBy($"{sortColumn} {sortOrder}");
        }

        public static string SortColumn(this string columnName)
        {
            if (string.IsNullOrEmpty(columnName) ||
                !LinqDynamicExtensions.IsValidProperty<City>(columnName, true))
            {
                return "name";
            }
            return columnName;
        }
    }
}
