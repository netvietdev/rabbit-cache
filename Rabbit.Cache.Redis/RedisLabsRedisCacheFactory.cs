using Rabbit.Integrations.Redis;
using StackExchange.Redis;
using System.Collections.Generic;

namespace Rabbit.Cache.Redis
{
    public class RedisLabsRedisCacheFactory : ICacheFactory
    {
        private static ConnectionMultiplexer _connection;

        /// <summary>
        /// Create a MemcachedCache instance
        /// </summary>
        /// <param name="options">
        /// The dictionary must contain below keys
        /// 1. endPoint
        /// 2. password
        /// </param>
        public ICache Create(IDictionary<string, string> options)
        {
            TryInitializeConnection(options);
            var db = _connection.GetDatabase();
            return new RedisCache(db);
        }

        private void TryInitializeConnection(IDictionary<string, string> options)
        {
            if (_connection != null)
            {
                return;
            }

            var endPoint = options["endPoint"];
            var password = options["password"];
            _connection = ConnectionMultiplexerManager.GetCurrent(endPoint, password);
        }
    }
}