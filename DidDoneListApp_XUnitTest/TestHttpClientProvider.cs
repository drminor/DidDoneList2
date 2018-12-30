using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WebApiClientLib;

namespace DidDoneListApp_XUnitTest
{
    public class TestHttpClientProvider : IHttpClientProvider
    {
        private HttpClient _httpClient;

        public TestHttpClientProvider(HttpClient instance)
        {
            _httpClient = instance;
        }

        public HttpClient GetNewHttpClient()
        {
            return _httpClient;
        }
    }
}
