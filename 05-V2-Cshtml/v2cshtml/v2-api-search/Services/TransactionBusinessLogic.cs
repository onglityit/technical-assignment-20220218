using Darren.Base.Model;
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
            TransactionResultListInfo lsInfo = await idb.sp_transactionrecord_query(ConstValues.TransactionsByDateRange, dateFrom_yyyyMMddTHHmmss, dateTo_yyyyMMddTHHmmss);
            return lsInfo;
        }
    }
}
