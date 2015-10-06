using System;

namespace Rabbit.Cache
{
    /// <summary>
    /// Cache contracts
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// Get from cache by given key
        /// </summary>
        T Get<T>(string key) where T : class;

        /// <summary>
        /// Put a value into the cache
        /// </summary>
        bool Set<T>(string key, T value) where T : class;

        /// <summary>
        /// Put a value into the cache
        /// </summary>
        bool Set<T>(string key, T value, TimeSpan expiry) where T : class;

        /// <summary>
        /// Remove from cache by a given key
        /// </summary>
        bool Remove(string key);
    }
}
