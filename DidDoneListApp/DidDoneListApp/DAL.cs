using DidDoneListModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using WebApiClientLib;

namespace DidDoneListApp
{
    public class DAL
    {
        //http://192.168.1.125:8080/api/customers/2

        public const string BASE_URI_HOST = "192.168.1.125";
        public const int BASE_URI_PORT_NUMBER = 8080;

        public const string CUSTOMER_END_POINT = "api/Customers";

        public async System.Threading.Tasks.Task<Uri> TestCreateCustomerAsync()
        {
            HttpClient client = TheClient;
            WebApiClient<Customers> customerWAClient = new WebApiClient<Customers>(CUSTOMER_END_POINT);

            Customers record = new Customers();
            Uri uri = await customerWAClient.CreateRecordAsync(record, client);
            return uri;
        }

        public async System.Threading.Tasks.Task<Customers> TestGetCustomerAsync(string id)
        {
            HttpClient client = TheClient;
            WebApiClient<Customers> customerWAClient = new WebApiClient<Customers>(CUSTOMER_END_POINT);

            Customers record = await customerWAClient.GetRecordAsync(id, client);
            return record;
        }

        private HttpClient _theClient;
        private HttpClient TheClient
        {
            get
            {
                if(_theClient == null)
                {
                    string baseUri = $"http://{BASE_URI_HOST}:{BASE_URI_PORT_NUMBER}/";

                    _theClient = new HttpClient
                    {
                        BaseAddress = new Uri(baseUri)
                    };

                    MediaTypeWithQualityHeaderValue mediaType = new MediaTypeWithQualityHeaderValue("application/json");

                    _theClient.DefaultRequestHeaders.Accept.Clear();
                    _theClient.DefaultRequestHeaders.Accept.Add(mediaType);
                }
                return _theClient;
            }
        }
    }
}
