using System.Collections.Generic;

namespace Rabbit.Cache.Impl
{
    /// <summary>
    /// Instantiate new InMemoryCache instance
    /// </summary>
    public class InMemoryCacheFactory : ICacheFactory
    {
        public ICache Create(IDictionary<string, string> options = null)
        {
            return new InMemoryCache();
        }
    }
}