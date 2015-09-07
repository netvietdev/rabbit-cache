using System.Collections.Generic;

namespace Rabbit.Cache
{
    public interface ICacheFactory
    {
        ICache Create(IDictionary<string, string> options = null);
    }
}