using adayca.ui.BaseInterface;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adayca.ui.Services
{
    public class CustomMemoryCache : ICustomMemoryCache
    {
        private readonly IMemoryCache _memoryCache;

        public CustomMemoryCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public object Get(object key)
        {
            return _memoryCache.Get(key);
        }

        public void Remove(object key)
        {
            _memoryCache.Remove(key);
        }

        public void Set(object key, string value)
        {
            _memoryCache.Set(key, value);
        }
    }
}
