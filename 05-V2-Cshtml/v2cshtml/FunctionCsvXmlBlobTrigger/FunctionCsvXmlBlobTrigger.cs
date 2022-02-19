using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Darren.Base;
using Darren.Base.Model;
using FunctionCsvXmlBlobTrigger.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FunctionCsvXmlBlobTrigger
{
    public class FunctionCsvXmlBlobTrigger
    {
        [FunctionName("FunctionCsvXmlBlobTrigger")]
        public async Task Run([BlobTrigger("international-transaction/{blobName}.{blobExtension}", 
                    Connection = "AzureWebJobsStorage")]Stream myBlob,
                    string blobName,
                    string blobExtension,
                    string blobTrigger, // full path to triggering blob
                    Uri uri, // blob primary location
                    ILogger log)

        {

            var config = new ConfigurationBuilder()
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            string SqlConnectionString = config["SqlConnectionString"];

            if (blobTrigger.Contains("good-file"))
            {
                if (blobExtension.ToUpper() == ConstValues.CSV)
                {
                    ExpandLineItemCSV ecsv = new ExpandLineItemCSV();
                    List<CsvTransactionItemBase> lsCsv = await ecsv.GetCsvRows(myBlob);
                    RecordInsertionService rec = new RecordInsertionService(config);

                }
            }
        }
    }
}
