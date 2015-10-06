using System.Collections.Generic;

namespace Rabbit.Cache
{
    /// <summary>
    /// ICache factory contracts
    /// </summary>
    public interface ICacheFactory
    {
        /// <summary>
        /// Create new instance of ICache with optional parameters
        /// </summary>
        ICache Create(IDictionary<string, string> options = null);
    }
}