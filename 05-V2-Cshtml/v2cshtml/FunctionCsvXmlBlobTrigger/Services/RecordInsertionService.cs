﻿using Darren.Base;
using Darren.Base.Model.XmlModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionCsvXmlBlobTrigger.Services
{
    public class RecordInsertionService
    {
        private readonly IConfigurationRoot config;
        private readonly DbWriteRecordServices dbs;
        private readonly int batchSize = 1;
        public RecordInsertionService(IConfigurationRoot _config)
        {
            config = _config;
            dbs = new DbWriteRecordServices(config);
        }

        public async Task InsertCsvItems(List<CsvTransactionItemBase> lsCsv,
            string blobName, string blobExtension, string uri)
        {            
            int linenumber = 0;
            string fileguid = GetGuidFromBlobName(blobName);
            if (batchSize == 1)
            {
                decimal crecAmount = 0;
                DateTime crecTransactionDate = DateTime.Now;
                foreach (var cRec in lsCsv) {
                    Decimal.TryParse(cRec.Amount, out crecAmount);
                    crecTransactionDate = DateTime.ParseExact(cRec.TransactionDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    await dbs.sp_transactionrecord_add_01(
                        fileguid,
                        ++linenumber,
                        uri,
                        blobExtension,
                        cRec.TransactionId,
                        crecAmount,
                        cRec.CurrencyCode,
                        crecTransactionDate,
                        cRec.Status);

                }
            }
        }
        public async Task InsertXmlItems(List<TransactionXML> lsXml,
            string blobName, string blobExtension, string uri)
        {            
            int linenumber = 0;
            string fileguid = GetGuidFromBlobName(blobName);
            if (batchSize == 1)
            {
                decimal crecAmount = 0;
                DateTime crecTransactionDate = DateTime.Now;
                foreach (var cRec in lsXml) {
                    Decimal.TryParse(cRec.PaymentDetails.Amount, out crecAmount);
                    crecTransactionDate = DateTime.ParseExact(cRec.TransactionDate, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
                    await dbs.sp_transactionrecord_add_01(
                        fileguid,
                        ++linenumber,
                        uri,
                        blobExtension,
                        cRec.Id,
                        crecAmount,
                        cRec.PaymentDetails.CurrencyCode,
                        crecTransactionDate,
                        cRec.Status);

                }
            }
        }

        public string GetGuidFromBlobName(string blobName)
        {
            return blobName.Split("/").Last();
        }
    }
}
