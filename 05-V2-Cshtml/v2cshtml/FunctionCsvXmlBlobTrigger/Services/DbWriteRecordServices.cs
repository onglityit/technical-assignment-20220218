using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionCsvXmlBlobTrigger.Services
{
    public class DbWriteRecordServices
    {
        private readonly IConfigurationRoot config;
        public DbWriteRecordServices(IConfigurationRoot _config)
        {
            config = _config;
        }
        public async Task<string> sp_transactionrecord_add_01(
            string fileguid ,
            int linenumber ,
            string bloburi ,
            string transactionid ,
            string amount ,
            string currencycode ,
            string transactiondate ,
            string statuscode ,
            bool istest = false
            )
        {
            string ret = string.Empty;
            using (var conn = new SqlConnection())
            {

            }
            return ret;
        }
    }
}
