using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using System.Collections.Generic;
using System.Net;

namespace Rabbit.Cache.Memcached
{
    /// <summary>
    /// Create a MemcachedCache instance from RedisLabs
    /// </summary>
    public class RedisLabsMemcachedCacheFactory : ICacheFactory
    {
        private static MemcachedClient _memcachedClient;

        /// <summary>
        /// Create a MemcachedCache instance
        /// </summary>
        /// <param name="options">
        /// The dictionary must contain below keys
        /// 1. hostName
        /// 2. port
        /// 3. userName
        /// 4. password
        /// Optional keys
        /// 1. zone
        /// </param>
        public ICache Create(IDictionary<string, string> options)
        {
            TryInitializeClient(options);
            return new MemcachedCache(_memcachedClient);
        }

        private static void TryInitializeClient(IDictionary<string, string> options)
        {
            if (_memcachedClient != null)
            {
                return;
            }

            var port = int.Parse(options["port"]);
            var userName = options["userName"];
            var password = options["password"];
            var zone = options.ContainsKey("zone") ? options["zone"] : string.Empty;
            var addresses = Dns.GetHostAddresses(options["hostName"]);

            var config = new MemcachedClientConfiguration();
            config.Servers.Add(new IPEndPoint(addresses[0], port));
            config.Protocol = MemcachedProtocol.Binary;
            config.Authentication.Type = typeof(PlainTextAuthenticator);
            config.Authentication.Parameters["userName"] = userName;
            config.Authentication.Parameters["password"] = password;
            config.Authentication.Parameters["zone"] = zone;

            _memcachedClient = new MemcachedClient(config);
        }
    }
}