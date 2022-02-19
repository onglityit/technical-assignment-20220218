using Darren.Base;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionCsvXmlBlobTrigger.Services
{
    public class RecordInsertionService
    {
        private readonly IConfigurationRoot config;
        private readonly DbWriteRecordServices dbs;
        public RecordInsertionService(IConfigurationRoot _config)
        {
            config = _config;
            dbs = new DbWriteRecordServices(config);
        }

        public async Task InsertCsvItems(List<CsvTransactionItemBase> lsCsv,
            string blobName)
        {
            int batchSize = 1;
            int linenumber = 0;
            string fileguid = GetGuidFromBlobName(blobName);
            if (batchSize == 1)
            {
                foreach (var cRec in lsCsv) {
                    dbs.sp_transactionrecord_add_01(cRec.);

                    ,
            int linenumber,
            string bloburi,
                }
            }
        }

        public string GetGuidFromBlobName(string blobName)
        {
            return blobName.Split("/").Last();
        }
    }
}
