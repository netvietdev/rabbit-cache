using Rabbit.Integrations.Redis;
using StackExchange.Redis;
using System;

namespace Rabbit.Cache.Redis
{
    public class RedisCache : ICache
    {
        private readonly IDatabase _database;

        public RedisCache(IDatabase database)
        {
            _database = database;
        }

        public T Get<T>(string key) where T : class
        {
            if (typeof(T) == typeof(string))
            {
                return _database.Get(key) as T;
            }

            return _database.Get<T>(key);
        }

        public bool Set<T>(string key, T value) where T : class
        {
            return _database.Set(key, value);
        }

        public bool Set<T>(string key, T value, TimeSpan expiry) where T : class
        {
            return _database.Set(key, value, expiry);
        }

        public bool Remove(string key)
        {
            return _database.KeyDelete(key);
        }
    }
}
