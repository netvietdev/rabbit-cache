using Enyim.Caching;
using Enyim.Caching.Memcached;
using System;

namespace Rabbit.Cache.Memcached
{
    public class MemcachedCache : ICache
    {
        private readonly IMemcachedClient _memcachedClient;

        /// <summary>
        /// Initialize the cache which uses default configuration of Memcached
        /// </summary>
        public MemcachedCache()
            : this(new MemcachedClient())
        {
        }

        public MemcachedCache(IMemcachedClient memcachedClient)
        {
            _memcachedClient = memcachedClient;
        }

        public T Get<T>(string key) where T : class
        {
            return (T)_memcachedClient.Get(key);
        }

        public bool Set<T>(string key, T value) where T : class
        {
            return _memcachedClient.Store(StoreMode.Set, key, value);
        }

        public bool Set<T>(string key, T value, TimeSpan expiry) where T : class
        {
            return _memcachedClient.Store(StoreMode.Set, key, value, expiry);
        }

        public bool Remove(string key)
        {
            return _memcachedClient.Remove(key);
        }
    }
}
