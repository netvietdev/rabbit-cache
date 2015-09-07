using System.Collections.Generic;

namespace Rabbit.Cache.Impl
{
    public class InMemoryCacheFactory : ICacheFactory
    {
        public ICache Create(IDictionary<string, string> options = null)
        {
            return new InMemoryCache();
        }
    }
}