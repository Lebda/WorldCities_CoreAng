using System.Linq;
using WorldCities.Models.RequestFeatures;
using System.Linq.Dynamic.Core;
using WorldCities.Implementations.RequestFeatures;

namespace WorldCities.Implementations.Repository
{
    public static class RepositoryExtensions
    {

        public static IQueryable<T> Filter<T>(this IQueryable<T> source, RequestParameters input)
            where T : class
        {
            string filterQuery = input.QueryMetaData.FilterQuery;
            string filterColumn = DefaultColumn<T>(input.QueryMetaData.FilterColumn);
            if (
                string.IsNullOrWhiteSpace(filterQuery) ||
                string.IsNullOrWhiteSpace(filterColumn) ||
                !LinqDynamicExtensions.IsValidFilterQuery(filterQuery))
            {
                return source;
            }

            return source.Where(string.Format("{0}.Contains(@0)", filterColumn), filterQuery);
        }

        public static IQueryable<T> Sort<T>(this IQueryable<T> source, RequestParameters input)
            where T : class
        {
            string sortColumn = DefaultColumn<T>(input.QueryMetaData.SortColumn);
            string sortOrder = LinqDynamicExtensions.SortOrder(input.QueryMetaData.SortOrder);
            return source.OrderBy($"{sortColumn} {sortOrder}");
        }

        public static string DefaultColumn<T>(this string columnName)
            where T : class
        {
            if (string.IsNullOrEmpty(columnName) ||
                !LinqDynamicExtensions.IsValidProperty<T>(columnName, true))
            {
                return "name";
            }
            return columnName;
        }
    }
}
