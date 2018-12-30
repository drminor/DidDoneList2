using System.Net.Http;

namespace WebApiClientLib
{
    public interface IHttpClientProvider
    {
        HttpClient GetNewHttpClient();
    }
}
