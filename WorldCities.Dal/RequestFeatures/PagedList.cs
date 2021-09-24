using System.Collections.Generic;

namespace WorldCities.Models.RequestFeatures
{
    public class PagedList<T> : List<T>
    {
        public ResponseMetaData MetaData { get; }

        public PagedList(List<T> items, int count, QueryMetaData metaData)
        {
            AddRange(items);
            MetaData = new ResponseMetaData(metaData, count);
        }

        //public static PagedList<T> ToPagedList(this IQueryable<T> source, int pageNumber, int
        //    pageSize)
        //{
        //    var count = source.Count();
        //    var items = source
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize).ToList();
        //    return new PagedList<T>(items, count, pageNumber, pageSize);
        //}
    }
}
