using System;

namespace WorldCities.Models.RequestFeatures
{
    public class ResponseMetaData
    {
        public QueryMetaData Query { get; }

        public ResponseMetaData(QueryMetaData query, int count)
        {
            Query = query;
            TotalItemsCount = count;
            TotalPagesCount = (int)Math.Ceiling(count / (double)query.PageSize);
        }
        public int TotalPagesCount { get; private set; }
        public int TotalItemsCount { get; set; }
        public bool HasPrevious => Query.PageIndex > Query.BaseIndex;
        public bool HasNext => Query.IsZeroBase ? ((Query.PageIndex + 1) < TotalPagesCount) : Query.PageIndex < TotalPagesCount;
        public string Info { get; set; }
    }
}
