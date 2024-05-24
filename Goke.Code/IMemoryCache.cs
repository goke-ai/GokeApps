using System.Collections.Generic;

namespace Microsoft.Extensions.Caching.Memory
{
    public interface IMemoryCache
    {
        void Remove(string key);
        void Set<T>(string key, List<T> list, object cacheOptions);
        bool TryGetValue<T>(string key, out List<T> list);
    }
}