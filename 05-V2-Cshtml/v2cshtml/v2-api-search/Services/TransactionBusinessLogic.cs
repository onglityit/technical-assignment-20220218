using Darren.Base.Model;
using System.Globalization;
using v2_api_search.Services.Interface;

namespace v2_api_search.Services
{
    public class TransactionBusinessLogic : ITransactionBusinessLogic
    {
        private readonly IDbQueryService idb;
        public TransactionBusinessLogic(IDbQueryService _idb)
        {
            idb = _idb;
        }
        public async Task<TransactionResultListInfo> TransactionsByCurrency(string currencycode)
        {
            TransactionResultListInfo lsInfo = await idb.sp_transactionrecord_query(ConstValues.TransactionsByCurrency, currencycode, String.Empty);
            return lsInfo;
        }
        public async Task<TransactionResultListInfo> TransactionsByStatus(string statuscode)
        {
            TransactionResultListInfo lsInfo = await idb.sp_transactionrecord_query(ConstValues.TransactionsByStatus, statuscode, String.Empty);
            return lsInfo;
        }
        public async Task<TransactionResultListInfo> TransactionsByDateRange(string dateFrom_yyyyMMddTHHmmss, string dateTo_yyyyMMddTHHmmss)
        {
            DateTime isoDt01 = DateTime.MinValue;
            DateTime isoDt02 = DateTime.MinValue;

            string strDt01 = string.Empty;
            string strDt02 = string.Empty;
            if(DateTime.TryParseExact(dateFrom_yyyyMMddTHHmmss, "yyyyMMddTHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out isoDt01)
                && DateTime.TryParseExact(dateTo_yyyyMMddTHHmmss, "yyyyMMddTHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out isoDt02))
            {
                strDt01 = isoDt01.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                strDt02 = isoDt02.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                TransactionResultListInfo lsInfo = await idb.sp_transactionrecord_query(ConstValues.TransactionsByDateRange, strDt01, strDt02);
                return lsInfo;
            }
            return new TransactionResultListInfo
            {
                isSuccess = false,
                errorMessage = "Please use the exact format of yyyyMMddTHHmmss no space and symbols -/:. "
            };
        }
    }
}
