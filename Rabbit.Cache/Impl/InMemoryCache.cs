using System;
using System.Runtime.Caching;

namespace Rabbit.Cache.Impl
{
    public class InMemoryCache : ICache
    {
        private readonly MemoryCache _cacheInstance;

        public InMemoryCache()
            : this(MemoryCache.Default)
        {
        }

        public InMemoryCache(MemoryCache cacheInstance)
        {
            _cacheInstance = cacheInstance;
        }

        public T Get<T>(string key) where T : class
        {
            var valueInCache = _cacheInstance.Get(key);

            if (typeof(T) == typeof(string))
            {
                return valueInCache as T;
            }

            if (valueInCache == null)
            {
                return default(T);
            }

            return (T)valueInCache;
        }

        public bool Set<T>(string key, T value) where T : class
        {
            _cacheInstance.Set(key, value, new CacheItemPolicy());

            return true;
        }

        public bool Set<T>(string key, T value, TimeSpan expiry) where T : class
        {
            _cacheInstance.Set(key, value, new CacheItemPolicy()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.Add(expiry)
            });

            return true;
        }

        public bool Remove(string key)
        {
            _cacheInstance.Remove(key);

            return true;
        }
    }
}
