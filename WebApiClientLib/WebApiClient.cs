using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApiClientLib
{
    public class WebApiClient<T> where T : class
    {
        private readonly string _endPointUri;

        public WebApiClient(string endPointUri)
        {
            _endPointUri = endPointUri;
        }

        // Get All Records
        public async Task<IEnumerable<T>> GetListAsync(HttpClient client)
        {
            IEnumerable<T> list = null;

            string path = _endPointUri;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadAsAsync<IEnumerable<T>>();
            }
            return list;
        }

        // TODO: Get All Records with a Where Clause

        // Get Record by simple key
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

        // TODO: Support compound keys

        // Create Record
        public async Task<Uri> CreateRecordAsync(T record, HttpClient client)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(_endPointUri, record);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        // TODO: Update Record

        // TODO: Delete Record

    }
}

