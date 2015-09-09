using System.Net;
using Microsoft.WindowsAzure.Storage.Table;

namespace Rabbit.Cache.AzureTables
{
    public static class TableResultExtensions
    {
        public static bool IsSuccess(this TableResult result)
        {
            return result.HttpStatusCode >= (int)HttpStatusCode.OK && result.HttpStatusCode < 300;
        }
    }
}