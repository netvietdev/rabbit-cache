using System;
using System.Net;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Rabbit.Cache.AzureTables
{
    public class AzureTablesCache : ICache
    {
        private readonly CloudTableClient _tableClient;

        public AzureTablesCache(string connectionString)
        {
            _tableClient = CreateCloudTableClient(connectionString);
        }

        private CloudTableClient CreateCloudTableClient(string connectionString)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            return storageAccount.CreateCloudTableClient();
        }

        public T Get<T>(string key) where T : class
        {
            var table = TryGetTable();

            var retrieveOperation = TableOperation.Retrieve<CacheEntity<T>>(typeof(CacheEntity<>).Name, key);
            var retrievedResult = table.Execute(retrieveOperation);

            if (retrievedResult.IsSuccess())
            {
                var cacheEntity = (CacheEntity<T>)retrievedResult.Result;
                return cacheEntity.Object;
            }

            return default(T);
        }

        public bool Remove(string key)
        {
            var table = TryGetTable();

            var retrieveOperation = TableOperation.Retrieve<CacheEntity<TableOperation>>(typeof(CacheEntity<>).Name, key);
            var retrievedResult = table.Execute(retrieveOperation);

            if (retrievedResult.IsSuccess())
            {
                var deleteEntity = (TableEntity)retrievedResult.Result;
                var deleteOperation = TableOperation.Delete(deleteEntity);
                var deleteResult = table.Execute(deleteOperation);
                return deleteResult.IsSuccess();
            }

            return true;
        }

        public bool Set<T>(string key, T value, TimeSpan expiry) where T : class
        {
            return Set(key, value);
        }

        public bool Set<T>(string key, T value) where T : class
        {
            var table = TryGetTable();

            var retrieveOperation = TableOperation.Retrieve<CacheEntity<T>>(typeof(CacheEntity<>).Name, key);
            var retrievedResult = table.Execute(retrieveOperation);

            if (retrievedResult.IsSuccess())
            {
                var updateEntity = (CacheEntity<T>)retrievedResult.Result;
                updateEntity.Object = value;

                var replaceOperation = TableOperation.Replace(updateEntity);
                var result = table.Execute(replaceOperation);

                return result.IsSuccess();
            }
            else
            {
                var insertOperation = TableOperation.Insert(new CacheEntity<T>()
                {
                    Object = value,
                    RowKey = key
                });
                var result = table.Execute(insertOperation);
                return result.HttpStatusCode == (int)HttpStatusCode.OK;
            }
        }

        private CloudTable TryGetTable()
        {
            var table = _tableClient.GetTableReference(typeof(AzureTablesCache).Name);
            table.CreateIfNotExists();
            return table;
        }
    }
}
