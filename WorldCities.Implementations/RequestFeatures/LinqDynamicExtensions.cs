using System;
using System.Reflection;

namespace WorldCities.Implementations.RequestFeatures
{
    public static class LinqDynamicExtensions
    {
        public static string SortOrder(this string sortOrder)
        {
            return
                !string.IsNullOrEmpty(sortOrder)
                    && sortOrder.ToUpper() == "DESC"
                    ? "DESC"
                    : "ASC";
        }

        public static bool IsValidFilterQuery(this string filterQuery)
        {
            return !string.IsNullOrEmpty(filterQuery);
        }

        public static bool IsValidProperty<T>(
            string propertyName,
            bool throwExceptionIfNotFound = true)
            where T : class
        {
            var prop = typeof(T).GetProperty(
                propertyName,
                BindingFlags.IgnoreCase |
                BindingFlags.Public |
                BindingFlags.Instance);
            if (prop == null && throwExceptionIfNotFound)
                throw new NotSupportedException(
                    string.Format(
                        "ERROR: Property '{0}' does not exist.",
                        propertyName)
                    );
            return prop != null;
        }
    }
}
