namespace WorldCities.Models.RequestFeatures
{
    public class QueryMetaData
    {
        private int pageSize = 10;
        private int pageIndex = 10;
        private int startIndex => IsZeroBase ? 0 : 1;

        public int MaxPageSize { get; }

        public QueryMetaData(int maxPageSize = 100)
        {
            MaxPageSize = maxPageSize;
        }

        public int BaseIndex => startIndex;
        public bool IsZeroBase { get; set; } = true;
        public int PageIndex
        {
            get => pageIndex;
            set => pageIndex = (value <= startIndex) ? startIndex : value;
        }
        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        /// <summary>
        /// Sorting Column name (or null if none set)
        /// </summary>
        public string SortColumn { get; set; }

        /// <summary>
        /// Sorting Order ("ASC", "DESC" or null if none set)
        /// </summary>
        public string SortOrder { get; set; }

        /// <summary>
        /// Filter Column name (or null if none set)
        /// </summary>
        public string FilterColumn { get; set; }
        /// <summary>
        /// Filter Query string 
        /// (to be used within the given FilterColumn)
        /// </summary>
        public string FilterQuery { get; set; }
    }
}
