using DidDoneListModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using WebApiClientLib;

namespace DidDoneListApp
{
    public class DAL
    {
        #region Private Properties

        private EndPointDetails _endPointDetails;

        #endregion

        #region Constructor

        public DAL(EndPointDetails endPointDetails)
        {
            _endPointDetails = endPointDetails;
        }

        #endregion

        #region Public Methods

        public async System.Threading.Tasks.Task<Uri> TestCreateCustomerAsync()
        {
            WebApiClient<Customers> customersWAClient = GetClient<Customers>(nameof(Customers));

            Customers record = new Customers
            {
                CustomerId = 0,
                FirstName = "New",
                LastName = "Test",
                StreetAddress = "1 North St.",
                City = "Raleigh",
                StateId = 1,
                Zip = "27800"
            };


            HttpClient client = TheClient;

            Uri uri = await customersWAClient.CreateRecordAsync(record, client);
            return uri;
        }

        public async System.Threading.Tasks.Task<Customers> TestGetCustomerAsync(string id)
        {
            WebApiClient<Customers> customersWAClient = GetClient<Customers>(nameof(Customers));

            HttpClient client = TheClient;
            Customers record = await customersWAClient.GetRecordAsync(id, client);
            return record;
        }

        #endregion

        #region WebApiClient Services

        private Dictionary<string, object> _clients = new Dictionary<string, object>();
        private WebApiClient<T> GetClient<T>(string serviceName) where T: class
        {
            if(!_clients.ContainsKey(serviceName))
            {
                string endPointAddress = _endPointDetails[serviceName];
                WebApiClient<T> webApiClient = new WebApiClient<T>(endPointAddress);
                _clients.Add(serviceName, webApiClient);

                return webApiClient;
            }
            else
            {
                return (WebApiClient<T>)_clients[serviceName];
            }
        }

        private string GetEndPointAddress(string serviceName)
        {
            string endPointAddress = _endPointDetails[serviceName];

            //endPointAddress = $"{_endPointDetails.BaseUri}/{endPointAddress}";

            return endPointAddress;
        }

        private HttpClient _theClient;
        private HttpClient TheClient
        {
            get
            {
                if(_theClient == null)
                {
                    //string baseUri = $"http://{_endPointDetails.HostName}:{BASE_URI_PORT_NUMBER}/";

                    _theClient = new HttpClient
                    {
                        BaseAddress = new Uri(_endPointDetails.BaseUri)
                    };

                    MediaTypeWithQualityHeaderValue mediaType = new MediaTypeWithQualityHeaderValue("application/json");

                    _theClient.DefaultRequestHeaders.Accept.Clear();
                    _theClient.DefaultRequestHeaders.Accept.Add(mediaType);
                }
                return _theClient;
            }
        }

        #endregion
    }
}
