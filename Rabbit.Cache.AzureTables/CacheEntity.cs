using Microsoft.WindowsAzure.Storage.Table;

namespace Rabbit.Cache.AzureTables
{
    internal class CacheEntity<T> : TableEntity
    {
        public CacheEntity()
        {
            PartitionKey = typeof(CacheEntity<>).Name;
        }

        public T Object { get; set; }
    }
}