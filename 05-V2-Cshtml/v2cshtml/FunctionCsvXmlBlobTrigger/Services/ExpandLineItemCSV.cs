using CsvHelper;
using CsvHelper.Configuration;
using Darren.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionCsvXmlBlobTrigger.Services
{
    public class ExpandLineItemCSV
    {
        public async Task<List<CsvTransactionItemBase>> GetCsvRows(Stream strm)
        {
            List<CsvTransactionItemBase> lsCsvItem = new List<CsvTransactionItemBase>();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                Delimiter = ",",
            };
            using (var reader = new StreamReader(strm))
            using (var csv = new CsvReader(reader, config))
            {
                lsCsvItem = csv.GetRecords<CsvTransactionItemBase>().ToList();
            }

            return lsCsvItem;
        }
    }
}
