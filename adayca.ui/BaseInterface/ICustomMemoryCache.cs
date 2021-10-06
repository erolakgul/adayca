using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adayca.ui.BaseInterface
{
    public interface ICustomMemoryCache
    {
        void Set(object key, string value);
        object Get(object key);
        void Remove(object key);
    }
}
