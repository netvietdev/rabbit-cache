using System;

namespace Rabbit.Cache
{
    public static class CacheExtensions
    {
        /// <summary>
        /// Try to get item from the cache by its key.
        /// If the returning value is default one, the function will be executed.
        /// Otherwise, return value from the cache.
        /// </summary>
        public static T GetOrExecute<T>(this ICache cache, string key, Func<T> function) where T : class
        {
            var value = cache.Get<T>(key);
            return (value != (default(T))) ? value : function();
        }
    }
}