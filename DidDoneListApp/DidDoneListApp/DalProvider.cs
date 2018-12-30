using WebApiClientLib;

namespace DidDoneListApp
{
    public class DalProvider : IDalProvider
    {
        private DAL _dal = null;
        private readonly EndPointDetails EndPointDetails;
        private readonly IHttpClientProvider HttpClientProvider;

        public DalProvider(EndPointDetails endPointDetails, IHttpClientProvider httpClientProvider)
        {
            EndPointDetails = endPointDetails;
            HttpClientProvider = httpClientProvider;

            _dal = new DAL(EndPointDetails, HttpClientProvider);
        }

        public DAL DAL => _dal;
    }
}
