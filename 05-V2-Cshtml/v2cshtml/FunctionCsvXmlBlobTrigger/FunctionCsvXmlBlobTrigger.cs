using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Darren.Base;
using FunctionCsvXmlBlobTrigger.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionCsvXmlBlobTrigger
{
    public class FunctionCsvXmlBlobTrigger
    {
        [FunctionName("FunctionCsvXmlBlobTrigger")]
        public async Task Run([BlobTrigger("international-transaction/{name}", Connection = "AzureWebJobsStorage")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            string ext = name.Split(".").Last();
            if (name.Contains("good-file"))
            {
                ExpandLineItemCSV ecsv = new ExpandLineItemCSV();
                CsvTransactionItemBase item = await ecsv.GetCsvRows(myBlob);
            }



        }
    }
}
