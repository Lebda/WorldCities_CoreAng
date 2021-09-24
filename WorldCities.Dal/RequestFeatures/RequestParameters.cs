namespace WorldCities.Models.RequestFeatures
{
    public abstract class RequestParameters
    {
        public RequestParameters(string orderBy, QueryMetaData queryMetaData)
        {
            OrderBy = orderBy;
            QueryMetaData = queryMetaData;
        }

        public QueryMetaData QueryMetaData { get; }

        public string OrderBy { get; set; }
        public string Fields { get; set; }
    }
}
