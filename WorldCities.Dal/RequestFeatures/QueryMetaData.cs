namespace WorldCities.Models.RequestFeatures
{
    public class QueryMetaData
    {
        private int pageSize = 10;
        private int pageIndex = 10;
        private int startIndex => IsZeroBase ? 0 : 1;

        public int MaxPageSize => 100;
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
    }
}
