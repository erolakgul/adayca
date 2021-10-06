using adayca.ui.BaseInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace adayca.ui.Services
{
    public class CustomHttpClient : ICustomHttpClient
    {
        HttpClient client_attribute;

        public CustomHttpClient()
        {
            Set();
        }

        public HttpClient Set()
        {
            client_attribute = new HttpClient();
            client_attribute.DefaultRequestHeaders.Accept.Clear();
            client_attribute.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client_attribute;
        }
    }
}
