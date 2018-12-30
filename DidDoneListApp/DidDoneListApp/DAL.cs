using DidDoneListModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebApiClientLib;

namespace DidDoneListApp
{
    public class DAL
    {
        #region Private Properties

        private EndPointDetails _endPointDetails;
        private readonly IHttpClientProvider _httpClientProvider;

        #endregion

        #region Constructor

        public DAL(EndPointDetails endPointDetails, IHttpClientProvider httpClientProvider)
        {
            _endPointDetails = endPointDetails ?? throw new ArgumentNullException(nameof(endPointDetails));
            _httpClientProvider = httpClientProvider ?? throw new ArgumentNullException(nameof(httpClientProvider));
        }

        #endregion

        #region Public Methods

        public async Task<Uri> CreateCustomerAsync(Customers record)
        {
            var customersClient = GetCustomersClient();

            using (HttpClient client = GetHttpClient())
            {
                Uri uri = await customersClient.CreateRecordAsync(record, client);
                return uri;
            }
        }

        public async Task<Customers> GetCustomerAsync(int id)
        {
            var customersClient = GetCustomersClient();

            using (HttpClient client = GetHttpClient())
            {
                Customers record = await customersClient.GetRecordAsync(id, client);
                return record;
            }
        }

        public async Task<IEnumerable<Customers>> GetCustomerListAsync(CancellationToken ct)
        {
            //WebApiClient<Customers, int> customersWAClient = GetClient<Customers, int>(nameof(Customers));

            //using (HttpClient client = _httpClientProvider.GetClient(_endPointDetails.BaseUri))
            //{
            //    IEnumerable<Customers> list = await customersWAClient.GetAllRecordsAsync(client, ct);
            //    return list;
            //}

            var customersClient = GetCustomersClient();

            using (HttpClient client = GetHttpClient())
            {
                IEnumerable<Customers> list = await customersClient.GetAllRecordsAsync(client, ct);
                return list;
            }
        }

        #endregion

        private WebApiClient<Customers, int> GetCustomersClient()
        {
            return GetClient<Customers, int>(nameof(Customers));
        }

        private WebApiClient<CustomerSite, int> GetCustomerSiteClient()
        {
            return GetClient<CustomerSite, int>(nameof(CustomerSite));
        }

        private HttpClient GetHttpClient()
        {
            return _httpClientProvider.GetNewHttpClient();
        }

        #region WebApiClient Services

        private Dictionary<string, object> _clients = new Dictionary<string, object>();

        private WebApiClient<T, IdType> GetClient<T, IdType>(string serviceName) where T: class
        {
            if(!_clients.ContainsKey(serviceName))
            {
                string endPointAddress = _endPointDetails[serviceName];
                WebApiClient<T, IdType> webApiClient = new WebApiClient<T, IdType>(endPointAddress);
                _clients.Add(serviceName, webApiClient);

                return webApiClient;
            }
            else
            {
                return (WebApiClient<T, IdType>)_clients[serviceName];
            }
        }

        private string GetEndPointAddress(string serviceName)
        {
            //endPointAddress = $"{_endPointDetails.BaseUri}/{endPointAddress}";

            string endPointAddress = _endPointDetails[serviceName];
            return endPointAddress;
        }

        //private HttpClient _theClient;
        //private HttpClient TheClient
        //{
        //    get
        //    {
        //        if(_theClient == null)
        //        {
        //            //string baseUri = $"http://{_endPointDetails.HostName}:{BASE_URI_PORT_NUMBER}/";

        //            _theClient = new HttpClient()
        //            {
        //                BaseAddress = new Uri(_endPointDetails.BaseUri)
        //            };

        //            MediaTypeWithQualityHeaderValue mediaType = new MediaTypeWithQualityHeaderValue("application/json");

        //            _theClient.DefaultRequestHeaders.Accept.Clear();
        //            _theClient.DefaultRequestHeaders.Accept.Add(mediaType);
        //        }
        //        return _theClient;
        //    }
        //}

        #endregion
    }
}
