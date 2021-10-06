using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace adayca.ui.BaseInterface
{
    public interface ICustomHttpClient
    {
        HttpClient Set();
    }
}
