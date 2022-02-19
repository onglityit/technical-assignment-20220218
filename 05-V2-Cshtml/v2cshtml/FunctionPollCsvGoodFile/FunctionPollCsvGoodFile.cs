using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionPollCsvGoodFile
{
    public class FunctionPollCsvGoodFile
    {
        [FunctionName("FunctionPollCsvGoodFile")]
        [StorageAccount("FunctionLevelStorageAppSetting")]
        public void Run([QueueTrigger("csv-good-file", Connection = "AzureWebJobsStorage")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            //expand transaction line items in this file uri

        }
    }
}
