using System.Collections.Generic;
using WorldCities.Models.RequestFeatures;

namespace WorldCities.Models.ResponseFeatures
{
    public class ApiResult<T>
        where T : class
    {
        public ApiResult(PagedList<T> pageList)
            : this(pageList, pageList.MetaData)
        {

        }

        private ApiResult(
            List<T> data,
            ResponseMetaData metaData)
        {
            Data = data;
            MetaData = metaData;

        }

        public List<T> Data { get; set; }
        public ResponseMetaData MetaData { get; }
    }
}
