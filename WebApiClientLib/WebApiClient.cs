using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApiClientLib
{
    public class WebApiClient<T> where T : class
    {

        private string _endPointUri;

        public WebApiClient(string endPointUri)
        {
            _endPointUri = endPointUri;
        }

        public async Task<Uri> CreateRecordAsync(T record, HttpClient client)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(_endPointUri, record);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        public async Task<T> GetRecordAsync(string Id, HttpClient client)
        {
            T record = null;

            string path = $"{_endPointUri}/{Id}";
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                record = await response.Content.ReadAsAsync<T>();
            }
            return record;
        }

    }
}

