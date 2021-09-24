namespace WorldCities.Models.RequestFeatures
{
    public abstract class RequestParameters
    {
        public RequestParameters(QueryMetaData queryMetaData)
        {
            QueryMetaData = queryMetaData;
        }

        public QueryMetaData QueryMetaData { get; }
    }
}
