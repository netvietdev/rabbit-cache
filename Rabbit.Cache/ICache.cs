using System;

namespace Rabbit.Cache
{
    public interface ICache
    {
        T Get<T>(string key) where T : class;

        bool Set<T>(string key, T value) where T : class;
        bool Set<T>(string key, T value, TimeSpan expiry) where T : class;

        bool Remove(string key);
    }
}
