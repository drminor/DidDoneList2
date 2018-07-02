using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClientLib
{
    public class WebApiClientLib<T> where T : class
    {

        //static HttpClient client = new HttpClient();

        private string _endPointUri;

        public WebApiClientLib(string endPointUri)
        {
            _endPointUri = endPointUri;
        }

        public async Task<Uri> CreateProductAsync(T record, HttpClient client)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(_endPointUri, record);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

    }
}
