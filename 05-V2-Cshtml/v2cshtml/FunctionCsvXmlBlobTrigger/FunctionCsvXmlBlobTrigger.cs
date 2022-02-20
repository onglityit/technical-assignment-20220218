using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Darren.Base;
using Darren.Base.Model;
using Darren.Base.Model.XmlModel;
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

            if (blobTrigger.Contains("good-file"))
            {
                if (blobExtension.ToUpper() == ConstValues.CSV)
                {
                    ExpandLineItemCSV ecsv = new ExpandLineItemCSV();
                    List<CsvTransactionItemBase> lsCsv = await ecsv.GetCsvRows(myBlob);
                    if(lsCsv != null && lsCsv.Count > 0)
                    {
                        RecordInsertionService rec = new RecordInsertionService(config);
                        await rec.InsertCsvItems(lsCsv, blobName, blobExtension, uri.ToString());
                    }

                }
                else if(blobExtension.ToUpper() == ConstValues.XML)
                {
                    ExpandTransactionModelXml exml = new ExpandTransactionModelXml();
                    List<TransactionXML> lsT = await exml.GetXmlArray(myBlob);
                    if(lsT != null && lsT.Count > 0)
                    {
                        RecordInsertionService rec = new RecordInsertionService(config);

                    }
                }
            }
        }
    }
}
