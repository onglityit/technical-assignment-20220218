using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionCsvXmlBlobTrigger
{
    public class FunctionCsvXmlBlobTrigger
    {
        [FunctionName("FunctionCsvXmlBlobTrigger")]
        public void Run([BlobTrigger("international-transaction/{name}", Connection = "azurite")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

        }
    }
}
