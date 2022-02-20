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
    }
}
