using System.Linq;
using WorldCities.Models.Models;
using WorldCities.Models.RequestFeatures;
using System.Linq.Dynamic.Core;
using WorldCities.Implementations.RequestFeatures;

namespace WorldCities.Implementations.Repository
{
    public static class CityRepositoryExtensions
    {

        public static IQueryable<City> Filter(this IQueryable<City> source, CityRequestParameters input)
        {
            string filterQuery = input.QueryMetaData.FilterQuery;
            string filterColumn = DefaultColumn(input.QueryMetaData.FilterColumn);
            if (
                string.IsNullOrWhiteSpace(filterQuery) ||
                string.IsNullOrWhiteSpace(filterColumn) ||
                !LinqDynamicExtensions.IsValidFilterQuery(filterQuery))
            {
                return source;
            }

            return source.Where(string.Format("{0}.Contains(@0)", filterColumn), filterQuery);
        }

        public static IQueryable<City> Sort(this IQueryable<City> source, CityRequestParameters input)
        {
            string sortColumn = DefaultColumn(input.QueryMetaData.SortColumn);
            string sortOrder = LinqDynamicExtensions.SortOrder(input.QueryMetaData.SortOrder);
            return source.OrderBy($"{sortColumn} {sortOrder}");
        }

        public static string DefaultColumn(this string columnName)
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
