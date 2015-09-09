using System;
using System.Collections.Generic;

namespace Rabbit.Cache.AzureTables
{
    public class AzureTablesCacheFactory : ICacheFactory
    {
        /// <summary>
        /// Create a cache instance
        /// </summary>
        /// <param name="options">
        /// The dictionary must contain below keys
        /// 1. connectionString
        /// </param>
        public ICache Create(IDictionary<string, string> options = null)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            if (!options.ContainsKey("connectionString"))
            {
                throw new ArgumentException("The options must contain the key connectionString");
            }

            return new AzureTablesCache(options["connectionString"]);
        }
    }
}