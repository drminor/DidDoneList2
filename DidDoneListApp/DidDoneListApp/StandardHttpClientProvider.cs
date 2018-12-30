using System;
using System.Net.Http;
using System.Net.Http.Headers;
using WebApiClientLib;

namespace DidDoneListApp
{
    public class StandardHttpClientProvider : IHttpClientProvider
    {
        private readonly string _baseUri;

        public StandardHttpClientProvider(string baseUri)
        {
            _baseUri = baseUri;
        }

        public HttpClient GetNewHttpClient()
        {
            HttpClient result = BuildClient(_baseUri);
            return result;
        }

        private HttpClient BuildClient(string baseUri)
        {
            HttpClient result = new HttpClient()
            {
                BaseAddress = new Uri(baseUri)
            };

            MediaTypeWithQualityHeaderValue mediaType = new MediaTypeWithQualityHeaderValue("application/json");

            result.DefaultRequestHeaders.Accept.Clear();
            result.DefaultRequestHeaders.Accept.Add(mediaType);

            return result;
        }
    }
}
