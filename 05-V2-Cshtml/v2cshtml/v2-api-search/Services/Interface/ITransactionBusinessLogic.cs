using Darren.Base.Model;

namespace v2_api_search.Services.Interface
{
    public interface ITransactionBusinessLogic
    {
        public Task<TransactionResultListInfo> TransactionsByCurrency(string currencycode);
        public Task<TransactionResultListInfo> TransactionsByStatus(string statuscode);
        public Task<TransactionResultListInfo> TransactionsByDateRange(string dateFrom_yyyyMMddTHHmmss, string dateTo_yyyyMMddTHHmmss);
    }
}
