﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
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
            string blobextension,
            string transactionid ,
            decimal amount ,
            string currencycode ,
            DateTime transactiondate ,
            string statuscode ,
            int istest = 0
            )
        {
            string ret = string.Empty;
            string strconn = config["SqlConnectionString"];
            using (var conn = new SqlConnection(strconn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "sp_transactionrecord_add_01";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;

                    if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                    cmd.Parameters.Add(new SqlParameter("@fileguid", SqlDbType.NVarChar) { Value = fileguid });
                    cmd.Parameters.Add(new SqlParameter("@linenumber", SqlDbType.Int) { Value = linenumber });
                    cmd.Parameters.Add(new SqlParameter("@bloburi", SqlDbType.NVarChar) { Value = bloburi });
                    cmd.Parameters.Add(new SqlParameter("@blobextension", SqlDbType.NVarChar) { Value = blobextension });
                    cmd.Parameters.Add(new SqlParameter("@transactionid", SqlDbType.NVarChar) { Value = transactionid });
                    cmd.Parameters.Add(new SqlParameter("@amount", SqlDbType.Decimal) { Value = amount });
                    cmd.Parameters.Add(new SqlParameter("@currencycode", SqlDbType.NChar) { Value = currencycode });
                    cmd.Parameters.Add(new SqlParameter("@transactiondate", SqlDbType.DateTime2) { Value = transactiondate });
                    cmd.Parameters.Add(new SqlParameter("@statuscode", SqlDbType.NVarChar) { Value = statuscode });
                    cmd.Parameters.Add(new SqlParameter("@istest", SqlDbType.Int) { Value = istest });

                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    if (reader.Read())
                    {
                        string isSuccess = reader["is_success"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    //log.LogInformation(ex.Message);

                }
            }
            return ret;
        }
    }
}
