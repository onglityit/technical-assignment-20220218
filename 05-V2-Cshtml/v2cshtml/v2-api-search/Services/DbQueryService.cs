using Darren.Base.Model;
using System.Data;
using System.Data.SqlClient;
using v2_api_search.Services.Interface;

namespace v2_api_search.Services
{
    public class DbQueryService : IDbQueryService
    {
        private readonly IConfiguration config;
        public DbQueryService(IConfiguration _config)
        {
            config = _config;
        }


        public async Task<TransactionResultListInfo> sp_transactionrecord_query(
            string querymode, string value01 , string value02 
            )
        {
            TransactionResultListInfo ret = new TransactionResultListInfo()
            {
                isSuccess = true,
                errorMessage = String.Empty,
            };
            List<TransactionResultModel> lsTr = new List<TransactionResultModel>();
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
                    cmd.Parameters.Add(new SqlParameter("@querymode", SqlDbType.NVarChar) { Value = querymode });
                    cmd.Parameters.Add(new SqlParameter("@value01", SqlDbType.NVarChar) { Value = value01 });
                    cmd.Parameters.Add(new SqlParameter("@value02", SqlDbType.NVarChar) { Value = value02 });

                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    int dataRows = 0;
                    String isSuccess = String.Empty;
                    while (reader.Read())
                    {
                        isSuccess = reader["is_success"].ToString();
                        if(isSuccess == "1")
                        {
                            //errormessage
                            TransactionResultModel tr1 = new TransactionResultModel() { 
                                Id = reader["id"].ToString(),
                                Payment = reader["payment"].ToString(),
                                Status = reader["status"].ToString(),
                            };
                            lsTr.Add(tr1);
                            dataRows++;
                        }
                        else
                        {
                            ret.isSuccess = false;
                            ret.errorMessage += reader["errormessage"].ToString();
                            break;
                        }
                    }
                    if (dataRows == 0)
                    {
                        ret.isSuccess = false;
                        ret.errorMessage += "Data not found, please check your input. ";

                    }
                    else
                    {
                        ret.lsTr = lsTr;
                        ret.isSuccess = true;
                    }
                }
                catch (Exception ex)
                {
                    ret.isSuccess = false;
                    ret.errorMessage = ex.Message;
                    //log.LogInformation(ex.Message);
                }
            }
            return ret;

        }
    }
}
