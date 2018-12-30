using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiClientLib
{
    public class WebApiClient<RecType, IdType> where RecType : class
    {
        private readonly string _endPointUri;
        private readonly Func<IdType, string> _toStringFunc;

        public WebApiClient(string endPointUri) : this(endPointUri, null)
        {
        }

        public WebApiClient(string endPointUri, Func<IdType, string> toStringFunc)
        {
            _endPointUri = endPointUri;
            _toStringFunc = toStringFunc;
        }

        // Get All Records
        //public async Task<IEnumerable<T>> GetListAsync(HttpClient client, CancellationToken ct)
        //{
        //    IEnumerable<T> list = null;

        //    string path = _endPointUri;
        //    HttpResponseMessage response = await client.GetAsync(path, ct);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        list = await response.Content.ReadAsAsync<IEnumerable<T>>(ct);
        //    }
        //    return list;
        //}

        public async Task<IEnumerable<RecType>> GetAllRecordsAsync(HttpClient client, CancellationToken ct)
        {
            IEnumerable<RecType> list = null;

            string path = _endPointUri;

            using (var request = new HttpRequestMessage(HttpMethod.Get, path))
            using (var response = await client.SendAsync(request, ct))
            {
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<IEnumerable<RecType>>(content);
                }
                return list;
            }
        }



        //----- DOES NOT WORK ----
        //HttpClient client = new HttpClient();
        //        client.BaseAddress = uri;
        //            client.DefaultRequestHeaders.Accept.Clear();
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //HttpResponseMessage response = client.PostAsJsonAsync(route, requestResource).Result;

        //------


        //---- Works ----

        //var request = new HttpRequestMessage(HttpMethod.Post, route);
        //        var hc = new StringContent(requestResource); // requestResource should be a string
        //        hc.Headers.ContentType = new MediaTypeHeaderValue("application/json);
        //request.Content = hc;
        //        var response = await httpClient.SendAsync(request);

        // TODO: Get All Records with a Where Clause

        // Get Record by simple key
        public async Task<RecType> GetRecordAsync(IdType id, HttpClient client)
        {
            RecType record = null;

            string path = $"{_endPointUri}/{GetFormattedId(id)}";
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                record = await response.Content.ReadAsAsync<RecType>();
            }
            return record;
        }

        private string GetFormattedId(IdType id)
        {
            string result = _toStringFunc == null ? id.ToString() : _toStringFunc(id);
            //if(_toStringFunc != null)
            //{
            //    result = _toStringFunc(id);
            //}
            //else
            //{
            //    result = id.ToString();
            //}

            return result;
        }

        // TODO: Support compound keys

        // Create Record
        public async Task<Uri> CreateRecordAsync(RecType record, HttpClient client)
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

